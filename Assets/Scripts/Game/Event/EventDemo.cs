
using Vocore;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public static class EventDemo
{
    //EventId用于标记事件的类型
    //示例EventId；EventManager.Generate传的参数是Event的StringId，不可重复，只运行使用数字，字母和点.
    public static readonly EventId DemoEvent = EventManager.Generate("DemoEvent");
}
