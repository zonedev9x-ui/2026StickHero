using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    public CameraSmooth cameraSmooth;
    public Tower towerPrefab;
    public Floor floorPrefab;
    public Player playerPrefab;
    public ItemSupport itemSupportPrefab;
    public Weapon weaponPrefab;
    public List<Trap> trapPrefabs;
    public List<Enemy> enemyPrefabs;
    public List<Enemy> bossPrefabs;

    public Transform tranStart;
    public float towerSpacingX = 10f;
    public float bossSpacingX = 15f;
    public float floorSpacingY = 3f;

    private List<Tower> towers = new List<Tower>();
    private Player player;
    private Enemy boss;
    private int currentTowerIndex = 1;

    private void Awake()
    {
        GameData.Load();
        LoadLevel(0);
    }

    #region Init Level

    private void LoadLevel(int levelIndex)
    {
        if (GameData.staticGameData == null) return;

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

        player = Instantiate(playerPrefab, playerFloor.SetPlayerPos(), Quaternion.identity, playerFloor.transform);
        player.InitCharacterScore(levelData.playerData.strengthScore);

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

                for (int slotIndex = 0; slotIndex < slotDatas.Count; slotIndex++)
                {
                    if (slotDatas[slotIndex].enemyName != EnemyName.None)
                    {
                        for (int enemyIndex = 0; enemyIndex < enemyPrefabs.Count; enemyIndex++)
                        {
                            if (enemyPrefabs[enemyIndex].enemyName == slotDatas[slotIndex].enemyName)
                            {
                                Transform spawnPoint = newFloor.spawnPos[slotIndex];

                                Enemy newEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity, newFloor.transform);
                                newEnemy.InitCharacterScore(slotDatas[slotIndex].strengthScore);

                                newFloor.entities.Add(newEnemy);
                            }
                        }
                    }
                    else if (slotDatas[slotIndex].itemSuportType != ItemSuportType.None)
                    {
                        ItemSupport newItemSupport = Instantiate(itemSupportPrefab, newFloor.spawnPos[slotIndex].position, Quaternion.identity, newFloor.transform);
                        newItemSupport.InitItemSupport(slotDatas[slotIndex].itemSuportType, slotDatas[slotIndex].strengthType, slotDatas[slotIndex].strengthScore);
                        newFloor.entities.Add(newItemSupport);
                    }
                    else if (slotDatas[slotIndex].weaponType != WeaponType.None)
                    {
                        Weapon newWeapon = Instantiate(weaponPrefab, newFloor.spawnPos[slotIndex].position, Quaternion.identity, newFloor.transform);
                        newWeapon.InitWeapon(slotDatas[slotIndex].weaponType, slotDatas[slotIndex].strengthType, slotDatas[slotIndex].strengthScore);
                        newFloor.entities.Add(newWeapon);
                    }
                    else if (slotDatas[slotIndex].trapType != TrapType.None)
                    {
                        for (int trapIndex = 0; trapIndex < trapPrefabs.Count; trapIndex++)
                        {
                            if (trapPrefabs[trapIndex].trapType == slotDatas[slotIndex].trapType)
                            {
                                Trap newTrap = Instantiate(trapPrefabs[trapIndex], newFloor.spawnPos[slotIndex].position, Quaternion.identity, newFloor.transform);
                                newTrap.InitTrap(slotDatas[slotIndex].strengthType, slotDatas[slotIndex].strengthScore);
                                newFloor.entities.Add(newTrap);
                            }
                        }
                    }
                }

                newTower.floors.Add(newFloor);
            }

            towers.Add(newTower);
        }

        List<float> listTargetPosX = new List<float>();

        for (int i = 1; i < towers.Count; i++)
        {
            float middeX = (towers[i].centerPoint.position.x + towers[i - 1].centerPoint.position.x) / 2f;
            listTargetPosX.Add(middeX);
        }

        if (levelData.bossEnemyData != null)
        {
            BossData bossData = levelData.bossEnemyData;

            for (int i = 0; i < bossPrefabs.Count; i++)
            {
                if (bossPrefabs[i].enemyName == bossData.enemyName)
                {
                    Vector3 spawnPos = tranStart.position + new Vector3(bossSpacingX * (towers.Count), 0f, 0f);
                    boss = Instantiate(bossPrefabs[i], spawnPos, Quaternion.identity);
                    boss.InitCharacterScore(bossData.strengthScore);

                    float middeX = (towers[towers.Count - 1].centerPoint.position.x + boss.transform.position.x) / 2f;
                    listTargetPosX.Add(middeX);
                }
            }
        }

        cameraSmooth.InitCamera(listTargetPosX);

        if (towers.Count > 2)
        {
            cameraSmooth.MoveFromStartToEnd();
        }
    }

    private void ClearCurrentLevel()
    {

    }

    #endregion

    #region Game Play

    #endregion

    public Tower SetCurrentTower()
    {
        return towers[currentTowerIndex];
    }

    public void NextTower()
    {
        if (currentTowerIndex > towers.Count - 1) return;

        currentTowerIndex++;
    }

    public Enemy SetBossInLevel()
    {
        return boss;
    }

    public bool IsAllEntityInCurrentTowerCleaned()
    {
        for (int i = 0; i < towers[currentTowerIndex].floors.Count; i++)
        {
            if (towers[currentTowerIndex].floors[i].IsEntityCleaned() == false)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsAllEntityInLastTowerCleaned()
    {
        for (int i = 0; i < towers[towers.Count - 1].floors.Count; i++)
        {
            if (towers[towers.Count - 1].floors[i].IsEntityCleaned() == true)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsFloorInCurrentTower(Floor floor)
    {
        for (int i = 0; i < towers[currentTowerIndex].floors.Count; i++)
        {
            if (floor == towers[currentTowerIndex].floors[i])
            {
                return true;
            }
        }

        return false;
    }

    public void MoveCameraToNextTower()
    {
        cameraSmooth.MoveNextDistanceTargets();
    }
}
