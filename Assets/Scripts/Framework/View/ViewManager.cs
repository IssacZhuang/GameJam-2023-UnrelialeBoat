using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Vocore;

public class ViewManager : MonoBehaviour, IEventReciever
{
    private List<IView> _views = new List<IView>();
    private Canvas _canvas;
    private Vector2 _screenSize;
    private Canvas _uiCanvas;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        if (_canvas == null)
        {
            Debug.LogError("ViewManager 需要有Canvas组件");
        }
        Current.ViewManager = this;
        _uiCanvas = this.GetComponent<Canvas>();
        _screenSize = new Vector2(Screen.width, Screen.height);
        OnScreenResize();

    }

    public void Push(IView view)
    {
        _views.Add(view);
        view.transform.SetParent(transform, false);
    }

    public void Remove(IView view)
    {
        if (_views.Remove(view))
        {
            view.Destroy();
        }
    }

    public void RemoveTop()
    {
        if (_views.Count > 0)
        {
            IView view = _views[_views.Count - 1];
            _views.RemoveAt(_views.Count - 1);
            view.Destroy();
        }
    }

    public void RemoveByTag(params string[] tags)
    {
        for (int i = _views.Count - 1; i >= 0; i--)
        {
            IView view = _views[i];
            foreach (string tag in tags)
            {
                if (view.Tags.Contains(tag))
                {
                    _views.RemoveAt(i);
                    view.Destroy();
                    break;
                }
            }
        }
    }



    public void FixedUpdate()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            try
            {
                _views[i].OnTick();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            try
            {
                _views[i].OnUpdate();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        if (_screenSize.x != Screen.width || _screenSize.y != Screen.height)
        {
            _screenSize.x = Screen.width;
            _screenSize.y = Screen.height;
            OnScreenResize();
        }
    }

    public void OnDestroy()
    {
        Clear();
    }

    public void Clear()
    {
        foreach (IView view in _views)
        {
            try
            {
                view.Destroy();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
        _views.Clear();
    }

    public void SendGlobalEvent<T>(EventId eventId, T value)
    {
        for (int i = 0; i < _views.Count; i++)
        {
            try
            {
                _views[i].SendEvent<T>(eventId, value);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    private void OnScreenResize()
    {
        _uiCanvas.scaleFactor = _screenSize.x / 1920f;
    }
}
