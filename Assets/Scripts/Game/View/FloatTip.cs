using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Vocore;

public class FloatTip : BaseView<FloatTipConfig>
{
    public string Text
    {
        get
        {
            return _text.text;
        }
        set
        {
            _text.text = value;
        }
    }
    private float _t;
    private float _timer;
    private TMP_Text _text;
    private RectTransform _container;
    private Func<float, float> _curve = UtilsCurve.GenerateBizerLerpCurve(0.6f, 0.24f, 0.36f, 1.28f);

    public static void Pop(string configName, string text)
    {
        FloatTipConfig config = Content.GetConfig<FloatTipConfig>(configName);
        if (config == null)
        {
            Debug.LogError("找不到FloatTip配置：" + configName);
            return;
        }

        FloatTip tip = new FloatTip();
        tip.Initialize(config);
        tip.Text = text;
        Current.ViewManager.Push(tip);
    }

    public static void Pop(string text)
    {
        Pop("defaultFloatTip", text);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        _text = transform.Find("Container/Text").GetComponent<TMP_Text>();
        _container = transform.Find("Container").GetComponent<RectTransform>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _timer += Time.deltaTime;
        _t += Time.deltaTime / Config.moveDuration;
        _t = Mathf.Clamp01(_t);
        float tCurved = _curve(_t);
        float from = _container.sizeDelta.x / 2;
        float to = -from;
        //move x
        _container.anchoredPosition = new Vector2(Mathf.LerpUnclamped(from, to, tCurved), _container.anchoredPosition.y);

        if (_timer > Config.lifeTime)
        {
            Current.ViewManager.Remove(this);
        }
    }
}
