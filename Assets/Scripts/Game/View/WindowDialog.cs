using System;
using System.Text;
using Vocore;

using UnityEngine;
using UnityEngine.UI;

public class WindowDialog : BaseView<DialogConfig>
{
    private int _currentDialogIndex = -1;
    private int _characterIndex = 0;
    private float _readTimer = 0;
    private bool isReading = false;
    private StringBuilder _textBuilder = new StringBuilder();
    private Text _textContent;
    private Text _textSpeaker;
    private Button _btnBg;

    public override void OnCreate()
    {
        base.OnCreate();
        _textContent = transform.Find("Container/Content").GetComponent<Text>();
        _textSpeaker = transform.Find("Container/Speaker").GetComponent<Text>();

        _btnBg = transform.Find("BtnBg").GetComponent<Button>();
        _btnBg.onClick.AddListener(() =>
        {
            if (isReading)
            {
                StopRead();
                _textContent.text = Config.content[_currentDialogIndex].content;
            }
        });
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!isReading) return;
        if (Config.content.IsNullOrEmpty()) return;

        Dialog dialog = Config.content[_currentDialogIndex];

        if (_readTimer < Config.textReadInterval)
        {
            _readTimer = 0;
            if (_characterIndex < dialog.content.Length)
            {
                _textBuilder.Append(dialog.content[_characterIndex]);
                SetContent(_textBuilder.ToString());
            }
            else
            {
                StopRead();
            }
            _characterIndex++;
        }

        _readTimer += Time.deltaTime;
    }

    public void SetContent(string text)
    {
        _textContent.text = text;
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

    public void StartRead()
    {
        _characterIndex = 0;
        _readTimer = 0;
        isReading = true;
        _textBuilder.Clear();
        if (Config.defaultSpeaker.IsNullOrEmpty())
        {
            _textSpeaker.text = Config.content[_currentDialogIndex].speaker;
        }
        else
        {
            _textSpeaker.text = Config.defaultSpeaker;
        }
    }

    public void StopRead()
    {
        isReading = false;
    }
}
