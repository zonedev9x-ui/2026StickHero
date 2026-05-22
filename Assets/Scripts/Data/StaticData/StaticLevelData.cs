using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class StaticLevelData
{
    public List<LevelData> levelDatas;

    public StaticLevelData()
    {
        if (levelDatas == null)
            levelDatas = Resources.LoadAll<LevelData>("LevelData").ToList();
            Debug.Log("Load Level Data: " + levelDatas.Count);
    }

    public LevelData GetLevelDataIndex(int levelIndex)
    {
        for (int i = 0; i < levelDatas.Count; i++)
        {
            if (levelDatas[i].levelIndex == levelIndex)
            {
                Debug.Log("Get Level Data: " + levelDatas[i].name);
                return levelDatas[i];
            }
        }

        return null;
    }
}
