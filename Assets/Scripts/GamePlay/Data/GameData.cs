using UnityEngine;

public class GameData
{
    public static StaticGameData staticGameData = new StaticGameData();
    public static UserData userData = new UserData();

    public static void Reset()
    {
        staticGameData = new StaticGameData();
        userData = new UserData();
    }
}
