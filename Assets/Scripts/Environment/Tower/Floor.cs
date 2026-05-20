using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public List<Transform> spawnPos;
    public List<GameObject> highlights;
    public List<GameObject> walls;
    public List<GameObject> breakWalls;

    public List<Enemy> enemies;
    public bool isFloorEmpty = false;
    private bool isLock = false;
    private int currentEnemyIndex;

    private void Start()
    {
        for (int i = 0; i < highlights.Count; i++)
        {
            highlights[i].gameObject.SetActive(false);
        }

        CreateEnemy();
    }

    private void CreateEnemy()
    {
        if (enemies.Count <= 0) return;

        int startIndex = spawnPos.Count - enemies.Count;

        for (int i = 0; i < enemies.Count; i++)
        {
            int spawnIndex = startIndex + i;
            enemies[i].transform.position = spawnPos[spawnIndex].position;

            Debug.Log("Enemy " + i + " spawn tai vi tri: " + spawnIndex);
        }
    }

    public void ShowHighLight()
    {
        highlights[0].gameObject.SetActive(true);
    }

    public void HideHighLight()
    {
        highlights[0].gameObject.SetActive(false);
    }

    public void ShowHighLightSelect()
    {
        highlights[1].gameObject.SetActive(true);
    }

    public void HideHighLightSelect()
    {
        highlights[1].gameObject.SetActive(false);
    }

    public bool IsLastEnemy()
    {
        return currentEnemyIndex == enemies.Count - 1;
    }

    public Transform GetTransformChild()
    {
        return spawnPos[currentEnemyIndex];
    }

    public Vector3 SetPlayerPos()
    {   
        if (currentEnemyIndex == enemies.Count - 1 || enemies.Count <= 0)
        {
            return spawnPos[1].position;
        }
        else
        {
            return spawnPos[currentEnemyIndex - 1].position;
        }
    }

    public Enemy GetCurrentEnemy()
    {
        if (enemies.Count <= 0 && currentEnemyIndex > enemies.Count - 1) return null;

        if (enemies[currentEnemyIndex].currentState == CharacterState.Dead)
        {
            return enemies[currentEnemyIndex++];
        }
        else
        {
            return enemies[currentEnemyIndex];
        }
    }

    public void BreakWalls()
    {
        for(int i = 0; i < walls.Count; i++)
        {
            walls[i].SetActive(false);
        }

        for (int i = 0; i < breakWalls.Count; i++)
        {
            breakWalls[i].SetActive(true);
        }
    }
}
