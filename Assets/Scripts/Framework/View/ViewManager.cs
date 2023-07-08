using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager: MonoBehaviour
{
    private List<IView> _views = new List<IView>();
    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        if (_canvas == null)
        {
            Debug.LogError("ViewManager 需要有Canvas组件");
        }

    }

    public void Push(IView view)
    {
        _views.Add(view);

        view.OnCreate();
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

    public void Clear()
    {
        foreach (IView view in _views)
        {
            view.Destroy();
        }
        _views.Clear();
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].OnTick();
        }
    }

    public void Update()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].OnUpdate();
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            _views[i].Destroy();
        }
    }
}
