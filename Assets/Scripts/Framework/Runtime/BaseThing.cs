using System;

using UnityEngine;

public class BaseThing<TConfig> : IEntity where TConfig : BaseConfig
{
    private TConfig _config;
    private GameObject _instance;
    public TConfig Config
    {
        get
        {
            return _config;
        }
    }

    public GameObject Instance
    {
        get
        {
            return _instance;
        }
    }

    public void Initialize(TConfig config, GameObject instance)
    {
        _config = config;
        _instance = instance;
        OnCreate();
    }

    public void Destroy()
    {
        OnDestroy();
        GameObject.Destroy(_instance);
    }

    public virtual void OnCreate()
    {

    }

    public virtual void OnDestroy()
    {

    }

    public virtual void OnTick()
    {

    }

    public virtual void OnUpdate()
    {

    }
}
