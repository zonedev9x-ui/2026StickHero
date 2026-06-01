using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Summit : MonoBehaviour
{
    public GameObject TopWall;
    public List<BreakWall> breakTopWalls;

    public void BreakWalls()
    {
        TopWall.SetActive(false);

        for (int i = 0; i < breakTopWalls.Count; i++)
        {
            breakTopWalls[i].gameObject.SetActive(true);
        }
    }

    public void MoveSummitDown(float posY)
    {
        transform.DOMoveY(transform.position.y - posY, 0.1f);
    }

    public void MoveSummitUp(float posY)
    {
        transform.DOMoveY(transform.position.y + posY, 0.1f);
    }
}
