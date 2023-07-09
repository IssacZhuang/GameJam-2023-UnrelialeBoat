using System;

public class BaseMapComponent: IEventReciever
{
    private MapConfig _config;
    private Map _map;

    public MapConfig Config
    {
        get { return _config; }
    }

    public Map Map
    {
        get { return _map; }
    }

    public void Init(Map map)
    {
        _map = map;
        _config = map.Config;
        OnCreate();
    }

    public virtual void OnCreate()
    {

    }

    public virtual void OnTick()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnDestroy()
    {

    }
}
