using System;

using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game _instance;
    public static Game Instance
    {
        get
        {
            return _instance;
        }
    }

    private Map _currentMap;
    public Map CurrentMap
    {
        get
        {
            return _currentMap;
        }
    }

    public string currentMap;

    public void LoadMap(MapConfig config)
    {
        if (_currentMap != null)
        {
            _currentMap.Destroy();
        }

        _currentMap = Map.CreateMap(config);
    }

    public void RelaodMap()
    {
        if (_currentMap == null)
        {
            return;
        }

        MapConfig config = _currentMap.Config;

        _currentMap.Destroy();
        _currentMap = Map.CreateMap(config);
    }

    private void Start()
    {
        _instance = this;

        MapConfig config = Content.GetConfig<MapConfig>(currentMap, false);

        if (config == null)
        {
            return;
        }

        LoadMap(config);
    }

    private void FixedUpdate()
    {
        if (_currentMap != null)
        {
            _currentMap.Tick();
        }
    }

    private void Update()
    {
        if (_currentMap != null)
        {
            _currentMap.Update();
        }
    }
}
