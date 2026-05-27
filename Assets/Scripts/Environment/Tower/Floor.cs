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
    public List<BreakWall> breakWalls;

    public List<Entity> entities;
    private bool isFloorEmpty = false;
    private bool isLock = false;
    public int currentEntityIndex;

    private void Start()
    {
        for (int i = 0; i < highlights.Count; i++)
        {
            highlights[i].gameObject.SetActive(false);
        }

        SortObjects();
    }

    //private void CreateEnemy()
    //{
    //    if (enemies.Count <= 0) return;

    //    int startIndex = spawnPos.Count - enemies.Count;

    //    for (int i = 0; i < enemies.Count; i++)
    //    {
    //        int spawnIndex = startIndex + i;
    //        enemies[i].transform.position = spawnPos[spawnIndex].position;

    //        Debug.Log("Enemy " + i + " spawn tai vi tri: " + spawnIndex);
    //    }
    //}

    private void SortObjects()
    {
        if (entities.Count <= 0) return;

        int startIndex = spawnPos.Count - entities.Count;

        for (int i = 0; i < entities.Count; i++)
        {
            int spawnIndex = startIndex + i;
            entities[i].transform.SetParent(spawnPos[spawnIndex]);
            entities[i].transform.localPosition = Vector3.zero;
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

    public Vector3 SetPlayerPos()
    {
        if (entities.Count <= 1)
        {
            return spawnPos[1].position;
        }
        else
        {
            return spawnPos[spawnPos.Count - 1 - entities.Count].position;
        }
    }

    public Entity GetCurrentEntity()
    {
        if (currentEntityIndex > entities.Count - 1) return null;

        return entities[currentEntityIndex];
    }

    public Entity GetNextEntity()
    {   
        if(entities.Count <= 0 && currentEntityIndex > entities.Count - 1) return null;

        return entities[currentEntityIndex++];
    }

    public bool IsLastEnemyInFloor()
    {
        if (currentEntityIndex == entities.Count)
        {
            return true;
        }
        return false;
    }

    public void BreakWalls()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].SetActive(false);
        }

        for (int i = 0; i < breakWalls.Count; i++)
        {
            breakWalls[i].gameObject.SetActive(true);
        }
    }
}
