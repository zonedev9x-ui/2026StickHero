using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : Singleton<TowerController>
{
    public List<Tower> towers;

    public int currentTowerIndex = 0;

    public Tower SetCurrentTower()
    {
        return towers[currentTowerIndex];
    }

    public bool IsFloorInCurrentTower(Floor floor)
    {
        for(int i = 0; i < towers[currentTowerIndex].floors.Count; i++)
        {
            if (floor == towers[currentTowerIndex].floors[i])
            {   
                Debug.Log("Floor is in current tower");
                return true;
            }
        }

        return false;
    }

    public bool IsLastTowerEmpty()
    {
        if (currentTowerIndex == towers.Count - 1)
        {
            if (towers[currentTowerIndex].IsAllFloorEmpty())
            {
                return true;
            }
        }
        return false;
    }
}
