using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float floorSpacingY = 3f;

    public Transform centerPoint;
    public Summit summit;
    public List<Floor> floors;

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
        for(int i = 0; i < floors.Count; i++)
        {
            if (floors[i].currentEntityIndex < floors[i].entities.Count)
            {
                return false;
            }
        }

        return true;
    }
}
