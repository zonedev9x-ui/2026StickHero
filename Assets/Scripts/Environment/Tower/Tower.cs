using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Summit summit;
    public List<Floor> floors;

    public void Init(List<Floor> floorList)
    {
        floors = floorList;
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

    public bool IsAllFloorEmpty()
    {
        for (int i = 0; i < floors.Count; i++)
        {
            if (floors[i].isFloorEmpty == true)
            {
                return true;
            }
        }
        return false;
    }

    public void BreakWalls()
    {
        summit.BreakWalls();

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].BreakWalls();
        }
    }
}
