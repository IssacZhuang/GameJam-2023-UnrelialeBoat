using System;
using System.Collections.Generic;
using Vocore;
using UnityEngine;
using UnityEngine.UI;

public class WindowEndGame : BaseView<BaseViewConfig>
{
    private Button _btn;
    private Image _image;
    private float _t = 0;
    public static void Pop(string configName = "WindowEndGame"){
        WindowEndGame window = new WindowEndGame();
        window.Initialize(Content.GetConfig<BaseViewConfig>(configName));
        Current.ViewManager.Push(window);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        _btn = transform.Find("Button").GetComponent<Button>();
        _image = transform.Find("Button").GetComponent<Image>();
        _btn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Current.SendGlobalEvent(EventCharacter.eventSetCharacterPaused, true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _image.color = new Color(1, 1, 1, _t);
        _t += Time.deltaTime;
        _t = Mathf.Clamp01(_t);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Current.SendGlobalEvent(EventCharacter.eventSetCharacterPaused, false);
    }
}
