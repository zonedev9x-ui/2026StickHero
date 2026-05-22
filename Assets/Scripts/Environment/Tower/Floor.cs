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

    public List<GameObject> objs;
    public bool isFloorEmpty = false;
    private bool isLock = false;
    private int currentObjIndex;

    private void Start()
    {
        for (int i = 0; i < highlights.Count; i++)
        {
            highlights[i].gameObject.SetActive(false);
        }
    }

    public void Init(List<SlotData> slotDatas)
    {
        for(int i = 0; i < slotDatas.Count; i++)
        {
            //if ()
            //{
                
            //}
        }
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

    //public bool IsLastEnemy()
    //{
    //    return currentObjIndex == objs.Count - 1;
    //}

    //public Transform GetTransformChild()
    //{
    //    return spawnPos[currentObjIndex];
    //}

    public Vector3 SetPlayerPos()
    {
        if (currentObjIndex == objs.Count - 1 || objs.Count <= 0)
        {
            return spawnPos[1].position;
        }
        else
        {
            return spawnPos[currentObjIndex - 1].position;
        }
    }

    //public Enemy GetCurrentEnemy()
    //{
    //    if (enemies.Count <= 0 && currentEnemyIndex > enemies.Count - 1) return null;

    //    if (enemies[currentEnemyIndex].currentState == CharacterState.Dead)
    //    {
    //        return enemies[currentEnemyIndex++];
    //    }
    //    else
    //    {
    //        return enemies[currentEnemyIndex];
    //    }
    //}

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
