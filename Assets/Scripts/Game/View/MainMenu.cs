using System;
using System.Text;
using Vocore;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;

public class MainMenu : BaseView<MainMenuConfig>
{
    private Button _btnStart;
    private Button _btnAbout;
    private Button _btnExit;

    private bool _isHide = false;

    public static void PopMainMenu()
    {
        MainMenuConfig config = Content.GetConfig<MainMenuConfig>("MainMenu");
        if (config == null)
        {
            Debug.LogError("未定义包含<name>MainMenu</name>的config文件");
            return;
        }

        MainMenu menu = new MainMenu();
        menu.Initialize(config);
        Current.ViewManager.Push(menu);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        // start button logic
        _btnStart = transform.Find("StartButton").GetComponent<Button>();
        _btnStart.onClick.AddListener(() =>
        {
            // 换地图
            Current.Game.LoadMap(Config.defaultMap);
            this.Hide();
        });
        // start button logic
        _btnAbout = transform.Find("AboutButton").GetComponent<Button>();
        _btnAbout.onClick.AddListener(() =>
        {
            // 另一个ui
        });
        // start button logic
        _btnExit = transform.Find("ExitButton").GetComponent<Button>();
        _btnExit.onClick.AddListener(() =>
        {
            Application.Quit(); //爬！
        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isHide)
            {
                this.Show();
                _isHide=false;
            }
            else
            {
                this.Hide();
                _isHide = true;
            }
        }


    }
}
