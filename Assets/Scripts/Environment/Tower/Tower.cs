using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float floorSpacingY = 3f;

    public Transform centerPoint;
    public Summit summit;
    public List<Floor> floors;

    public void Start()
    {
        SortSummitAndFloors();
    }

    public void ShowAllHighlightNormal()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].ShowHighLight();
        }
    }

    public void HideAllHighlight()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].HideHighLight();
            floors[i].HideHighLightSelect();
        }
    }

    public void HideAllHighlightSelect()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].HideHighLightSelect();
        }
    }

    public void SortSummitAndFloors()
    {
        if (summit != null)
        {
            summit.gameObject.SetActive(true);
            summit.transform.localPosition = new Vector3(0f, floorSpacingY * floors.Count, 0f);
        }

        for (int i = 0; i < floors.Count; i++)
        {
            float heightY = floorSpacingY * (floors.Count - 1 - i);
            Vector3 floorSpawnPos = transform.position + new Vector3(0f, heightY, 0f);
            floors[i].transform.position = floorSpawnPos;
        }
    }

    public void SortSummitAndFloorsDown()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if (floors[i].gameObject.activeSelf == true && floors[i].IsEntityCleaned() == true)
            {
                floors[i].HideFloor();

                summit.MoveSummitDown(floorSpacingY);

                for (int j = 0; j < i; j++)
                {
                    floors[j].MoveFloorDown(floorSpacingY);
                }
            }
        }
    }

    public void SortSummitAndFloorsUp()
    {

    }

    public void BreakWalls()
    {
        summit.BreakWalls();

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].BreakWalls();
        }
    }

    public bool IsTowerCleaned()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if (floors[i].IsEntityCleaned() == false)
            {
                return false;
            }
        }

        return true;
    }
}
