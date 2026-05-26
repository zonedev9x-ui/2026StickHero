using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelIndex;
    public BossData bossEnemyData;
    public PlayerData playerData;
    public List<TowerData> towerDatas;
}
