using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelIndex;
    public EnemyData enemyBossData;
    public List<TowerData> towerDatas;
}
