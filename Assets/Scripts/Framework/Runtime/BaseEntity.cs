using System;

public class BaseEntity<TConfig> : IEntity where TConfig : BaseConfig
{
    private TConfig _config;
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
        OnCreate();
    }

    public void Destroy()
    {
        OnDestroy();
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
