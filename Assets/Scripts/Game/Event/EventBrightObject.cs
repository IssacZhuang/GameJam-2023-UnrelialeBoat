
using Vocore;
using UnityEditor;
using UnityEngine;

public static class EventObjectBrightnessAll
{
    public static EventId eventDiscoverObjectAll = EventManager.Generate("eventDiscoverObjectAll");
    //public static EventId eventDiscoverObjectLoopAll = EventManager.Generate("evenDiscoverObjectLoopAll");
    //public static EventId eventUndiscoverObjectAll = EventManager.Generate("evenUndiscoverObjectAll");
    
}


public static class EventObjectBrightness
{
    public static EventId eventDiscoverObject = EventManager.Generate("eventDiscoverObject");
    //public static EventId eventUndiscoverObject = EventManager.Generate("evenUndiscoverObject");
    //public static EventId eventDiscoverObjectLoop = EventManager.Generate("eventDiscoverObjectLoop");
    //public static EventId eventDiscoverLineObject = EventManager.Generate("eventDiscoverLineObject");
    //public static EventId eventUndiscoveLinerObject = EventManager.Generate("evenUndiscoveLinerObject");
    public static EventId eventDiscoverLineObjectLoop = EventManager.Generate("eventDiscoverLineObjectLoop");
}


//public static class EventObjecOutline
//{
//    public static EventId eventSetObjecOutlinet = EventManager.Generate("eventSetObjecOutlinet");
//}