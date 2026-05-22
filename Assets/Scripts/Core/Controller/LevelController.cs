using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public Tower towerPrefab;
    public Floor floorPrefab;
    public Player playerPrefab;
    public List<Enemy> enemies;

    public Transform tranStart;
    public float towerSpacingX = 3f;
    public float floorSpacingY = 3f;

    public List<Tower> towers;
    private int currentTowerIndex = 1;

    private void Awake()
    {   
        GameData.Load();

        LoadLevel(0);
    }

    #region Init Level

    private void LoadLevel(int levelIndex)
    {
        if(GameData.staticGameData == null) return;

        LevelData levelData = GameData.staticGameData.staticLevelData.GetLevelDataIndex(levelIndex);

        Tower playerTower = Instantiate(towerPrefab, tranStart.position, Quaternion.identity);
        List<Floor> playerFloorList = new List<Floor>();
        
        Floor playerFloor = Instantiate(floorPrefab, playerTower.transform.position, Quaternion.identity, playerTower.transform);
        playerFloorList.Add(playerFloor);
        playerTower.Init(playerFloorList);
        towers.Add(playerTower);

        Player player = Instantiate(playerPrefab, playerFloor.SetPlayerPos(), Quaternion.identity, playerFloor.transform);

        for (int i = 0; i < levelData.towerDatas.Count; i++)
        {
            Vector3 spawnPos = tranStart.position + new Vector3(towerSpacingX * (i + 1), 0f, 0f);
            Tower newTower = Instantiate(towerPrefab, spawnPos, Quaternion.identity);
 
            if (newTower.summit != null)
            {
                newTower.summit.gameObject.SetActive(true);
                newTower.summit.transform.localPosition = new Vector3(0f, floorSpacingY * levelData.towerDatas[i].floorDatas.Count, 0f);
            }

            List<Floor> currentTowerFloors = new List<Floor>();

            int floorCount = levelData.towerDatas[i].floorDatas.Count;
            for (int j = 0; j < floorCount; j++)
            {
                float heightY = floorSpacingY * (floorCount - 1 - j);
                Vector3 floorSpawnPos = newTower.transform.position + new Vector3(0f, heightY, 0f);
                
                Floor newFloor = Instantiate(floorPrefab, floorSpawnPos, Quaternion.identity, newTower.transform);
                newFloor.Init(levelData.towerDatas[i].floorDatas[j].slotDataList);              
                currentTowerFloors.Add(newFloor);
            }

            newTower.Init(currentTowerFloors);
            towers.Add(newTower);
        }
    }

    private void ClearCurrentLevel()
    {

    }

    #endregion

    public Tower SetCurrentTower()
    {
        return towers[currentTowerIndex];
    }

    public bool IsFloorInCurrentTower(Floor floor)
    {
        for (int i = 0; i < towers[currentTowerIndex].floors.Count; i++)
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
