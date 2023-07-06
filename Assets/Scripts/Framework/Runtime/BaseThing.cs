using System;

using UnityEngine;
using Vocore;

public class BaseThing<TConfig> : IEntity, IEventReciever where TConfig : BaseConfig
{
    private TConfig _config;
    private bool _isSpawned = false;
    private GameObject _instance;
    private Map _map;

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

    public Map Map
    {
        get
        {
            return _map;
        }
    }

    public bool IsSpawned
    {
        get
        {
            return _isSpawned;
        }
    }

    #region 管理创建和销毁，不可重写以确保安全

    /// <summary>
    /// 物体初始化
    /// </summary>
    public void Initialize(TConfig config, GameObject instance)
    {
        _config = config;
        _instance = instance;
        OnCreate();
    }

    /// <summary>
    /// 物体生成到地图上
    /// </summary>
    public void Spawn(Map map)
    {
        _map = map;
        _isSpawned = true;
        OnSpawn();
        OnBindingEvent();
    }

    /// <summary>
    /// 物体从地图上移除
    /// </summary>
    public void Despawn()
    {
        _isSpawned = false;
        OnBindingEvent();
        OnDespawn();
        if (_instance != null)
        {
            GameObject.Destroy(_instance);
        }
        _config = null;
    }

    #endregion

    #region 可重写的生命周期方法

    /// <summary>
    ///当物体在内存里被创建，此时还没生成到地图上
    /// </summary>
    public virtual void OnCreate()
    {

    }

    /// <summary>
    /// 当物体生成到地图上
    /// </summary>
    public virtual void OnSpawn()
    {

    }

    /// <summary>
    /// 当物体从地图上移除
    /// </summary>
    public virtual void OnDespawn()
    {

    }

    /// <summary>
    /// 绑定事件，物体被生成到地图或者从地图上移除时会调用
    /// </summary>
    protected virtual void OnBindingEvent()
    {

    }

    /// <summary>
    /// 逻辑帧
    /// </summary>
    public virtual void OnTick()
    {

    }

    /// <summary>
    /// 渲染帧
    /// </summary>
    public virtual void OnUpdate()
    {

    }

    #endregion

    #region 功能

    //绑定事件
    protected void BindEvent<TData>(EventId eventId, Action<TData> callback)
    {
        if (_isSpawned)
        {
            this.RegisterEvent(eventId, callback);
        }
        else
        {
            this.UnregisterEvent(eventId);
        }
    }

    #endregion
}
