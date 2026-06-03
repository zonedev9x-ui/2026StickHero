using UnityEngine;

public class GameData
{
    public static StaticGameData staticGameData;

    public static void Load()
    {
        if (staticGameData == null) staticGameData = new StaticGameData();
    }
}
