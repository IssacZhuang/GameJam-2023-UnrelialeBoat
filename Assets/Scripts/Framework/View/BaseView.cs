using System;
using System.Collections.Generic;
using UnityEngine;

using Vocore;

public class BaseView<TConfig> :  IView where TConfig : BaseViewConfig
{
    private TConfig _config;
    private GameObject _instance;
    private HashSet<EventId> _eventIds = new HashSet<EventId>();
    private List<IView> _subViews = new List<IView>();

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

    #region 管理创建和销毁，不可重写以确保安全

    public void Initialize(TConfig config, GameObject instance)
    {
        _config = config;
        _instance = instance;
        OnCreate();
    }

    /// <summary>
    /// 物体初始化
    /// </summary>
    public void Initialize(TConfig config)
    {
        _config = config;
        _instance = Content.GetPrefabInstance(config.prefab);
        OnCreate();
    }

    /// <summary>
    /// 当UI被显示
    /// </summary>
    public void Show()
    {
        _instance.SetActive(true);
        OnShow();
    }

    /// <summary>
    /// 当UI被隐藏
    /// </summary>
    public void Hide()
    {
        _instance.SetActive(false);
        OnShow();
    }

    /// <summary>
    /// UI被销毁
    /// </summary>
    public void Destroy()
    {
        OnDestroy();
        if (_instance != null)
        {
            GameObject.Destroy(_instance);
        }
        _instance = null;
        _config = null;
        foreach (var eventId in _eventIds)
        {
            this.UnregisterEvent(eventId);
        }
        _eventIds.Clear();
        foreach (var subView in _subViews)
        {
            subView.Destroy();
        }
        _subViews.Clear();
    }

    #endregion

    #region 可重写的生命周期方法

    /// <summary>
    /// 当UI被创建
    /// </summary>
    public virtual void OnCreate()
    {

    }

    /// <summary>
    /// 当UI被销毁
    /// </summary>
    public virtual void OnDestroy()
    {

    }

    /// <summary>
    /// 当UI被显示
    /// </summary>
    public virtual void OnShow()
    {

    }

    /// <summary>
    /// 当UI被隐藏
    /// </summary>
    public virtual void OnHide()
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
        this.RegisterEvent(eventId, callback);
        _eventIds.Add(eventId);
    }

    protected void BindSubView<TSubView, TSubConfig>(GameObject subViewRoot, string configName) where TSubView : BaseView<TSubConfig> where TSubConfig : BaseViewConfig
    {
        TSubConfig config = Content.GetConfig<TSubConfig>(configName);
        BindSubView<TSubView, TSubConfig>(subViewRoot, config);
    }

    protected void BindSubView<TSubView, TSubConfig>(GameObject subViewRoot, TSubConfig config) where TSubView : BaseView<TSubConfig> where TSubConfig : BaseViewConfig
    {
        TSubView subView = Activator.CreateInstance<TSubView>();
        subView.Initialize(config, subViewRoot);
        AddSubView(subView);
    }

    private void AddSubView(IView view)
    {
        _subViews.Add(view);
    }

    #endregion
}
