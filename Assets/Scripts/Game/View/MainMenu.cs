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
    private GameObject _panelAbout;
    private Button _btnConfirm;

    private bool _isHide = false;

    public static void PopMainMenu()
    {
        MainMenuConfig config = Content.GetConfig<MainMenuConfig>("MainMenu");
        if (config == null)
        {
            Debug.LogError("δ�������<name>MainMenu</name>��config�ļ�");
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
            // ����ͼ
            Current.Game.LoadMap(Config.defaultMap);
            Current.AudioManager.PlayAsync("elevator");
            this.Hide();
            _isHide = true;
        });
        // start button logic
        _btnAbout = transform.Find("AboutButton").GetComponent<Button>();
        _btnAbout.onClick.AddListener(() =>
        {
            // ��һ��ui
            _panelAbout.SetActive(true);
        });
        // start button logic
        _btnExit = transform.Find("ExitButton").GetComponent<Button>();
        _btnExit.onClick.AddListener(() =>
        {
            Application.Quit(); //����
        });

        // about panel logic
        _panelAbout = transform.Find("AboutPage").gameObject;
        // about confirm button logic
        _btnConfirm = transform.Find("AboutPage").Find("AboutConfirmButton").GetComponent<Button>();
        _panelAbout.SetActive(false);
        _btnConfirm.onClick.AddListener(() =>
        {
            // hide panel
            _panelAbout.SetActive(false);
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
