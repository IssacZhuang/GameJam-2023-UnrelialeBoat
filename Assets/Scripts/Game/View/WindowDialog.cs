using System;
using System.Text;
using Vocore;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class WindowDialog : BaseView<DialogConfig>
{
    private int _currentDialogIndex = -1;
    private int _characterIndex = 0;
    private float _readTimer = 0;
    private bool isReading = false;
    private StringBuilder _textBuilder = new StringBuilder();
    private TMP_Text _textContent;
    private TMP_Text _textSpeaker;
    private Button _btnBg;

    public static void PopDialog(string name)
    {
        DialogConfig config = Content.GetConfig<DialogConfig>(name);
        if (config == null)
        {
            Debug.LogError("找不到对话框配置：" + name);
            return;
        }

        WindowDialog dialog = new WindowDialog();
        dialog.Initialize(config);
        Current.ViewManager.Push(dialog);
    }

    public override void OnCreate()
    {
        base.OnCreate();
        _textContent = transform.Find("Container/Content").GetComponent<TMP_Text>();
        _textSpeaker = transform.Find("Container/Speaker").GetComponent<TMP_Text>();

        _textContent.text = "";
        _textSpeaker.text = "";

        _btnBg = transform.Find("BtnBg").GetComponent<Button>();
        _btnBg.onClick.AddListener(() =>
        {
            if (isReading)
            {
                StopRead();
            }
            else
            {
                NextDialog();
            }
        });

        _currentDialogIndex = -1;
        NextDialog();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!isReading) return;
        if (Config.content.IsNullOrEmpty()) return;

        if (_readTimer >= Config.textReadInterval)
        {
            _readTimer = 0;
            NextCharacter();
        }

        _readTimer += Time.deltaTime;
    }

    public void SetContent(string text)
    {
        _textContent.text = text;
    }

    private void SetSpeaker(string text)
    {
        _textSpeaker.text = text;
    }

    public void NextDialog()
    {
        if (_currentDialogIndex < Config.content.Count - 1)
        {
            _currentDialogIndex++;
            StartRead();
        }
        else
        {
            Current.ViewManager.Remove(this);
        }
    }

    private void StartRead()
    {
        _characterIndex = 0;
        _readTimer = 0;
        isReading = true;
        _textBuilder.Clear();
        if (Config.defaultSpeaker.IsNullOrEmpty())
        {
            SetSpeaker(Config.content[_currentDialogIndex].speaker);
        }
        else
        {
            SetSpeaker(Config.defaultSpeaker);
        }
    }

    private void StopRead()
    {
        isReading = false;
        SetContent(Config.content[_currentDialogIndex].text);
    }

    private void NextCharacter()
    {
        Dialog dialog = Config.content[_currentDialogIndex];
        if (!dialog.text.IsNullOrEmpty() && _characterIndex < dialog.text.Length)
        {
            _textBuilder.Append(dialog.text[_characterIndex]);
            SetContent(_textBuilder.ToString());
        }
        else
        {
            StopRead();
        }
        _characterIndex++;
    }
}
