using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject TopWall;
    public List<BreakWall> breakTopWalls;
    public List<Floor> floors;

    private void OnEnable()
    {

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
        TopWall.SetActive(false);

        for (int i = 0; i < breakTopWalls.Count; i++)
        {
            breakTopWalls[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < floors.Count; i++)
        {
            floors[i].BreakWalls();
        }
    }
}
