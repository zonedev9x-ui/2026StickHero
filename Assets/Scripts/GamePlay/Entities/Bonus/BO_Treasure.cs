using DG.Tweening;
using UnityEngine;

public class BO_Treasure : BonusObject
{
    public Transform lidTreasure;
    public float rotationX;
    public float rotationSpeed;

    public override void TakeAction()
    {
        base.TakeAction();

        Vector3 currentRotation = lidTreasure.localEulerAngles;

        lidTreasure.DOLocalRotate(
            new Vector3(rotationX, currentRotation.y, currentRotation.z),
            rotationSpeed
        );
    }
}
