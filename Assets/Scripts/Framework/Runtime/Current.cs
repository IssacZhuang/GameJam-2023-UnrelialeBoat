using Vocore;
using UnityEngine;
public static class Current
{
    public static Game Game => Game.Instance;
    public static Map Map => Game.CurrentMap;
    public static CameraTrace CameraTrace { get; set; }
    public static ViewManager ViewManager { get; set; }
    public static AudioManager AudioManager { get; set; }
    public static WaveShaderController WaveShaderController { get; set; }
    public static Camera SceneCamera => WaveShaderController.sceneCamera;

    public static Character MainCharacter {
        get;set;
    }

    public static void SendGlobalEvent<T>(EventId eventId, T value)
    {
        Map?.SendGlobalEvent(eventId, value);
        ViewManager?.SendGlobalEvent(eventId, value);
    }
}
