using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{
    public float moveSpeed = 5f;

    public List<float> listTargetPosX;
    private int currentDistance = 0;

    public bool IsMoving { get; private set; }

    public void InitCamera(List<float> distanceTowers)
    {
        this.listTargetPosX = distanceTowers;
        transform.position = new Vector3(this.listTargetPosX[currentDistance], transform.position.y, transform.position.z);
    }

    public void MoveNextDistanceTargets()
    {
        if (listTargetPosX == null && listTargetPosX.Count <= 0) return;

        if (currentDistance < listTargetPosX.Count - 1)
        {
            currentDistance++;
            IsMoving = true;

            transform.DOMoveX(listTargetPosX[currentDistance], moveSpeed).OnComplete(() =>
            {
                IsMoving = false;
            });        
        }
    }

    public void MoveFromStartToEnd()
    {
        IsMoving = true;
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(0.6f);

        seq.Append(
            transform.DOMoveX(listTargetPosX[listTargetPosX.Count - 1], moveSpeed)
        );

        seq.AppendInterval(0.4f);

        seq.Append(
            transform.DOMoveX(listTargetPosX[currentDistance], moveSpeed)
        );

        seq.OnComplete(() =>
        {
            IsMoving = false;
        });
    }
}
