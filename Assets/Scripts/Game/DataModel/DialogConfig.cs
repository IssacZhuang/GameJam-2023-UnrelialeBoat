using System;
using System.Collections.Generic;

public class DialogConfig : BaseViewConfig
{
    public string defaultSpeaker;
    public float textReadInterval = 0.05f;
    public List<Dialog> content;
}
