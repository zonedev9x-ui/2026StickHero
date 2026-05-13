using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{   
    public int towerCount = 3;
    public Tower towerPrefabs;

    public List<Tower> towers;
    public int currentTowerIndex = 0;

    private void OnEnable()
    {
        for(int i = 0; i < towerCount; i++)
        {
            Tower tower = Instantiate(towerPrefabs, transform);
            towers.Add(tower);
        }
    }
}
