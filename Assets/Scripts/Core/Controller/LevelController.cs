using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public Tower towerPrefab;
    public Floor floorPrefab;
    public Player playerPrefab;
    public List<Enemy> enemyPrefabs;

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
        Floor playerFloor = Instantiate(floorPrefab, playerTower.transform.position, Quaternion.identity, playerTower.transform);
        playerTower.floors.Add(playerFloor);

        if (playerTower.summit != null)
        {
            playerTower.summit.gameObject.SetActive(true);
            playerTower.summit.transform.localPosition = new Vector3(0f, floorSpacingY, 0f);
        }

        towers.Add(playerTower);

        Player player = Instantiate(playerPrefab, playerFloor.SetPlayerPos(), Quaternion.identity, playerFloor.transform);

        for (int towerIndex = 0; towerIndex < levelData.towerDatas.Count; towerIndex++)
        {
            Vector3 spawnPos = tranStart.position + new Vector3(towerSpacingX * (towerIndex + 1), 0f, 0f);
            Tower newTower = Instantiate(towerPrefab, spawnPos, Quaternion.identity);

            if (newTower.summit != null)
            {
                newTower.summit.gameObject.SetActive(true);
                newTower.summit.transform.localPosition = new Vector3(0f, floorSpacingY * levelData.towerDatas[towerIndex].floorDatas.Count, 0f);
            }

            List<FloorData> floorDatas = levelData.towerDatas[towerIndex].floorDatas;

            for (int floorIndex = 0; floorIndex < floorDatas.Count; floorIndex++)
            {
                float heightY = floorSpacingY * (floorDatas.Count - 1 - floorIndex);
                Vector3 floorSpawnPos = newTower.transform.position + new Vector3(0f, heightY, 0f);

                Floor newFloor = Instantiate(floorPrefab, floorSpawnPos, Quaternion.identity, newTower.transform);

                List<SlotData> slotDatas = floorDatas[floorIndex].slotDatas;

                for(int slotIndex = 0; slotIndex < slotDatas.Count; slotIndex++)
                {
                    if (slotDatas[slotIndex].enemyName != EnemyName.None)
                    {
                        for(int enemyIndex = 0; enemyIndex < enemyPrefabs.Count; enemyIndex++)
                        {
                            if (enemyPrefabs[enemyIndex].enemyName == slotDatas[slotIndex].enemyName)
                            {
                                Transform spawnPoint = newFloor.spawnPos[slotIndex];

                                Enemy newEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, spawnPoint.rotation, newFloor.transform);
                            }
                        }
                    }
                }

                newTower.floors.Add(newFloor);


            }
         
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
