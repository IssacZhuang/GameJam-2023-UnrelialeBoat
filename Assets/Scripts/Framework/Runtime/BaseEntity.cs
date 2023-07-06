using System;

using Vocore;

public class BaseEntity<TConfig> : IEntity, IEventReciever where TConfig : BaseConfig
{
    private TConfig _config;
    private bool _isDestroyed = false;
    public TConfig Config
    {
        get
        {
            return _config;
        }
    }

    public void Initialize(TConfig config)
    {
        _config = config;
        _isDestroyed = false;
        OnCreate();
        OnBindingEvent();
    }

    public void Destroy()
    {
        _isDestroyed = true;
        OnBindingEvent();
        OnDestroy();
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
