using System;
using System.Collections.Generic;
using Vocore;
using UnityEngine;
using UnityEngine.UI;

public class WindowEndGame : BaseView<BaseViewConfig>
{
    private Button _btn;
    public static void Pop(string configName = "WindowEndGame"){
        WindowEndGame window = new WindowEndGame();
        window.Initialize(Content.GetConfig<BaseViewConfig>(configName));
        Current.ViewManager.Push(window);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        _btn = transform.Find("Button").GetComponent<Button>();
        _btn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Current.SendGlobalEvent(EventCharacter.eventSetCharacterPaused, true);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Current.SendGlobalEvent(EventCharacter.eventSetCharacterPaused, false);
    }
}
