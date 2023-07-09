using System;
using System.Collections.Generic;

public class DialogConfig : BaseViewConfig
{
    public string defaultSpeaker; // 默认说话者
    public float textReadInterval = 0.05f; // 文字阅读间隔
    public List<Dialog> content; // 对话内容
}
