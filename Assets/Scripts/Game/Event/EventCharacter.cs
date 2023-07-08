
using Vocore;
using UnityEditor;
using UnityEngine;

public static class EventCharacter
{
    public static EventId eventSetCharacterPaused = EventManager.Generate("eventSetCharacterPaused");
    public static EventId eventCharacterDetectObject = EventManager.Generate("eventCharacterDetectObject");
}
