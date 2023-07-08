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

    public void SendEvent(EventId eventId)
    {
        Entity?.SendEvent(eventId);
    }

    public static void SendEventByGameObject(GameObject obj, EventId eventId)
    {
        var bridge = obj.GetComponent<EventBridge>();
        if (bridge == null)
        {
            return;
        }

        bridge.SendEvent(eventId);

    }

    public static void SendEventByGameObject<T>(GameObject obj, EventId eventId, T data)
    {
        var bridge = obj.GetComponent<EventBridge>();
        if (bridge == null)
        {
            return;
        }

        bridge.SendEvent(eventId, data);
        
    }
}
