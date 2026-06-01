using DG.Tweening;
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
        if(entities.Count <= 0)
        {
            return spawnPos[1].position;
        }
        else if (entities.Count == 1)
        {
            if (entities[entities.Count - 1] is Enemy)
            {
                return spawnPos[1].position;
            }
            else
            {
                return spawnPos[spawnPos.Count - 1 - entities.Count].position;
            }
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
        currentEntityIndex++;

        if (entities.Count <= 0 || currentEntityIndex > entities.Count - 1) return null;

        return entities[currentEntityIndex];
    }

    public bool IsLastEnemyInFloor()
    {
        if (currentEntityIndex == entities.Count)
        {
            return true;
        }
        return false;
    }

    public void HideFloor()
    {
        gameObject.SetActive(false);
    }

    public void MoveFloorDown(float posY)
    {
        transform.DOMoveY(transform.position.y - posY, 0.1f);
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

    public bool IsEntityCleaned()
    {
        for(int i = 0; i < entities.Count; i++)
        {
            if (entities[i].IsActive() == true)
            {
                return false;
            }
        }

        return true;
    }
}
