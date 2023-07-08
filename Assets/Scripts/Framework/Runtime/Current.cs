public static class Current
{
    public static Game Game => Game.Instance;
    public static Map Map => Game.CurrentMap;

    public static Character MainCharacter {
        get;set;
    }
}
