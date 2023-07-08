using System;
using System.Collections.Generic;
using Vocore;
using UnityEngine;

public class EventBridge : MonoBehaviour
{
    public IEntity Entity { get; set; }

    public void SendEvent<T>(EventId eventId, T data)
    {
        Entity?.SendEvent(eventId, data);
    }

    public static void SendEvent<T>(GameObject obj, EventId eventId, T data)
    {
        var bridge = obj.GetComponent<EventBridge>();
        if (bridge == null)
        {
            return;
        }

        bridge.SendEvent(eventId, data);
        
    }
}
