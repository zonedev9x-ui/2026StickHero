using DG.Tweening;
using UnityEngine;

public class TrapSaw : Trap
{   
    public float rotationSpeed = 100f;
    public GameObject saw;

    private void Update()
    {
        saw.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }

    public override void Attack()
    {
        base.Attack();

        transform.DOScale(Vector3.zero, 0.5f);

        SetActive(false);
    }
}
