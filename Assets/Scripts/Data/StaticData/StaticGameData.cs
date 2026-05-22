using UnityEngine;

public class StaticGameData
{
    public StaticLevelData staticLevelData;

    public StaticGameData()
    {
        if (staticLevelData == null) staticLevelData = new StaticLevelData();
    }
}
