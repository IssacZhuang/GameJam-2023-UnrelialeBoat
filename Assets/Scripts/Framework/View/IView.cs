using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IView: IEventReciever
{
    IEnumerable<string> Tags { get; }
    RectTransform transform { get; }
    void Destroy();
    void Show();
    void Hide();
    void OnCreate();
    void OnUpdate();
    void OnTick();
}
