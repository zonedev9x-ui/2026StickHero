using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

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

    public List<Tower> towers = new List<Tower>();
    public List<Entity> entities = new List<Entity>();
    private Player player;
    private Enemy boss;
    public int currentTowerIndex = 1;
    private int levelCurrent = 0;
    public int entityCount = 0;

    private void Awake()
    {
        Instance = this;

        levelCurrent = GameData.userData.profile.currentStageId;
        LoadLevel(levelCurrent);
    }

    #region Init Level

    private void LoadLevel(int levelIndex)
    {
        if (GameData.staticGameData == null) return;

        LevelData levelData = GameData.staticGameData.staticLevelData.GetLevelDataIndex(levelIndex);

        SpawnPlayerTower(levelData);

        SpawnTowers(levelData);

        SpawnBossEnemy(levelData);

        SetupCamera();
    }

    private void SpawnPlayerTower(LevelData levelData)
    {
        Tower playerTower = Instantiate(towerPrefab, tranStart.position, Quaternion.identity);

        playerTower.SortSummitAndFloors();

        towers.Add(playerTower);

        player = Instantiate(
            playerPrefab,
            playerTower.floors[playerTower.floorCount].SetPlayerPos(),
            Quaternion.identity,
            playerTower.floors[playerTower.floorCount].transform
        );

        player.InitCharacterScore(levelData.playerData.strengthScore);
    }

    private void SpawnTowers(LevelData levelData)
    {
        for (int towerIndex = 0; towerIndex < levelData.towerDatas.Count; towerIndex++)
        {
            Vector3 spawnPos = tranStart.position + new Vector3(towerSpacingX * (towerIndex + 1), 0f, 0f);
            Tower newTower = Instantiate(towerPrefab, spawnPos, Quaternion.identity);

            List<FloorData> floorDatas = levelData.towerDatas[towerIndex].floorDatas;

            newTower.floorCount = floorDatas.Count;
            newTower.SortSummitAndFloors();

            SpawnFloors(floorDatas, newTower);

            towers.Add(newTower);
        }
    }

    private void SpawnFloors(List<FloorData> floorDatas, Tower tower)
    {
        for (int floorIndex = 0; floorIndex < floorDatas.Count; floorIndex++)
        {
            Floor newFloor = tower.floors[floorIndex];
            List<SlotData> slotDatas = floorDatas[floorIndex].slotDatas;

            for (int slotIndex = 0; slotIndex < slotDatas.Count; slotIndex++)
            {
                SpawnEntities(slotDatas[slotIndex], newFloor.entities, newFloor.spawnPos[slotIndex]);
            }
        }
    }

    private void SpawnEntities(SlotData slotData, List<Entity> entitiesInFloor, Transform spawnPoint)
    {
        if (slotData.enemyName != EnemyName.None)
        {
            for (int enemyIndex = 0; enemyIndex < enemyPrefabs.Count; enemyIndex++)
            {
                if (enemyPrefabs[enemyIndex].enemyName == slotData.enemyName)
                {
                    Enemy newEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity, spawnPoint.transform);
                    newEnemy.InitCharacterScore(slotData.strengthScore);
                    entitiesInFloor.Add(newEnemy);
                    entities.Add(newEnemy);
                }
            }
        }
        else if (slotData.itemSuportType != ItemSuportType.None)
        {
            ItemSupport newItemSupport = Instantiate(itemSupportPrefab, spawnPoint.position, Quaternion.identity, spawnPoint.transform);
            newItemSupport.InitItemSupport(slotData.itemSuportType, slotData.strengthType, slotData.strengthScore);

            entitiesInFloor.Add(newItemSupport);
            entities.Add(newItemSupport);
        }
        else if (slotData.weaponType != WeaponType.None)
        {
            Weapon newWeapon = Instantiate(weaponPrefab, spawnPoint.position, Quaternion.identity, spawnPoint.transform);
            newWeapon.InitWeapon(slotData.weaponType, slotData.strengthType, slotData.strengthScore);

            entitiesInFloor.Add(newWeapon);
            entities.Add(newWeapon);
        }
        else if (slotData.trapType != TrapType.None)
        {
            for (int trapIndex = 0; trapIndex < trapPrefabs.Count; trapIndex++)
            {
                if (trapPrefabs[trapIndex].trapType == slotData.trapType)
                {
                    Trap newTrap = Instantiate(trapPrefabs[trapIndex], spawnPoint.position, Quaternion.identity, spawnPoint.transform);
                    newTrap.InitTrap(slotData.strengthType, slotData.strengthScore);

                    entitiesInFloor.Add(newTrap);
                    entities.Add(newTrap);
                }
            }
        }

        entityCount++;
    }

    private void SpawnBossEnemy(LevelData levelData)
    {
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

                    entities.Add(boss);
                }
            }
        }
    }

    private void SetupCamera()
    {
        List<float> listTargetPosX = new List<float>();

        for (int i = 1; i < towers.Count; i++)
        {
            float middeX = (towers[i].centerPoint.position.x + towers[i - 1].centerPoint.position.x) / 2f;
            listTargetPosX.Add(middeX);
        }

        float bossMiddeX = (towers[towers.Count - 1].centerPoint.position.x + boss.transform.position.x) / 2f;
        listTargetPosX.Add(bossMiddeX);

        cameraSmooth.InitCamera(listTargetPosX);

        if (towers.Count > 2)
        {
            cameraSmooth.MoveFromStartToEnd();
        }
    }

    #endregion

    #region Logic Game

    public void CheckTowerProgress(Player player)
    {
        if (IsAllEntityInCurrentTowerCleaned() == true)
        {
            if (currentTowerIndex >= towers.Count - 1 && boss != null)
            {
                cameraSmooth.MoveLastTargetAndScale();

                player.UpdateChangeSize();
            }

            MoveCameraToNextTower();

            if (currentTowerIndex <= towers.Count - 1)
            {
                currentTowerIndex++;
            }
        }
    }

    public void CheckEndGame(Player player)
    {
        if (IsAllEntityInactive(entities) == true)
        {
            player.UpdateWin();
        }
    }

    public bool IsAllEntityInactive(List<Entity> entityList)
    {
        for (int i = 0; i < entityList.Count; i++)
        {
            if (entityList[i].isActive == true)
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region Logic Tower

    public Tower SetCurrentTower()
    {
        return towers[currentTowerIndex];
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

    public void UpdateTowers()
    {
        Tower currentTower = towers[currentTowerIndex];
        Tower prevTower = towers[currentTowerIndex - 1];

        for (int i = 0; i < currentTower.floors.Count; i++)
        {
            if (currentTower.floors[i].gameObject.activeSelf == true && currentTower.floors[i].IsEntityCleaned() == true)
            {
                currentTower.SortSummitAndFloorsDown(i);

                prevTower.SortSummitAndFloorsUp();
            }
        }
    }

    #endregion

    #region UI and Event

    public void SetEndGame(bool isWin)
    {
        if (isWin)
        {
            GameData.userData.profile.EndStage(isWin);
            LWin ui = UIManager.Instance.LoadUI(UIKey.WIN) as LWin;
        }
        else
        {
            GameData.userData.profile.EndStage(isWin);
            //LLose ui = UIManager.Instance.LoadUI(UIKey.LOSE) as LLose;
        }
    }

    #endregion

    public void MoveCameraToNextTower()
    {
        cameraSmooth.MoveNextDistanceTargets();
    }

    public Enemy SetBossInLevel()
    {
        return boss;
    }
}
