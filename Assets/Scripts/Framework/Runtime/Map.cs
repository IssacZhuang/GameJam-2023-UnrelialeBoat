using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public class Map
{
    private List<IEntity> _entities = new List<IEntity>();
    private HashSet<IEntity> _check = new HashSet<IEntity>();
    private GameObject _scene;
    private MapConfig _config;
    private List<BaseMapComponent> _components = new List<BaseMapComponent>();

    public MapConfig Config
    {
        get
        {
            return _config;
        }
    }

    public static Map CreateMap(MapConfig config)
    {
        Map map = new Map(config);
        return map;
    }

    public Map(MapConfig config)
    {
        _scene = Content.GetPrefabInstance(config.scene); ;
        _config = config;
        if (config.components != null)
        {
            for (int i = 0; i < config.components.Count; i++)
            {
                try
                {
                    BaseMapComponent component = Activator.CreateInstance(config.components[i]) as BaseMapComponent;
                    component.Init(this);
                    _components.Add(component);
                }
                catch
                {
                    Debug.LogError("无法创建MapComponent: " + config.components[i].Name);
                }
            }
        }
    }

    public GameObject Scene
    {
        get
        {
            return _scene;
        }
    }

    public bool AddEntity(IEntity entity)
    {
        if (_check.Contains(entity))
        {
            return false;
        }

        _check.Add(entity);
        _entities.Add(entity);
        entity.Spawn(this);
        return true;
    }

    public bool RemoveEntity(IEntity entity)
    {
        if (!_check.Contains(entity))
        {
            return false;
        }

        _check.Remove(entity);
        _entities.Remove(entity);
        entity.Despawn();
        return true;
    }

    public void Tick()
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            try
            {
                _entities[i].OnTick();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        for (int i = 0; i < _components.Count; i++)
        {
            try
            {
                _components[i].OnTick();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            try
            {
                _entities[i].OnUpdate();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        for (int i = 0; i < _components.Count; i++)
        {
            try
            {
                _components[i].OnUpdate();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public void Destroy()
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            try
            {
                _entities[i].Despawn();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        for (int i = 0; i < _components.Count; i++)
        {
            try
            {
                _components[i].OnDestroy();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        GameObject.Destroy(_scene);
    }
}
