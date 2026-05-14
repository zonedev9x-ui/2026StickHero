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
}
