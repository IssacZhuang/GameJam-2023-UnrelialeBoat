using Vocore;

public static class Current
{
    public static Game Game => Game.Instance;
    public static Map Map => Game.CurrentMap;
    public static CameraTrace CameraTrace { get; set; }
    public static ViewManager ViewManager { get; set; }

    public static Character MainCharacter {
        get;set;
    }

    public static void SendGlobalEvent<T>(EventId eventId, T value)
    {
        Map?.SendGlobalEvent(eventId, value);
        ViewManager?.SendGlobalEvent(eventId, value);
    }
}
