using UnityEngine;

public class StaticGameData
{
    public StaticLevelData staticLevelData;

    public void Load()
    {
        if (staticLevelData == null) staticLevelData = new StaticLevelData();
    }
}
