using System;

using UnityEngine;
using Vocore;

public class BaseThing<TConfig> : IEntity, IEventReciever where TConfig : BaseConfig
{
    private TConfig _config;
    private bool _isDestroyed = false;
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
        _isDestroyed = false;
        OnCreate();
        OnBindingEvent();
    }

    public void Destroy()
    {
        _isDestroyed = true;
        OnBindingEvent();
        OnDestroy();
        GameObject.Destroy(_instance);
        _config = null;
    }

    protected void BindEvent<TData>(EventId eventId, Action<TData> callback)
    {
        if (_isDestroyed)
        {
            this.UnregisterEvent(eventId);
        }
        else
        {
            this.RegisterEvent(eventId, callback);
        }
    }

    protected virtual void OnBindingEvent()
    {

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
