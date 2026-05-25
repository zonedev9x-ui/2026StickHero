using UnityEngine;

public class TrapSaw : Trap
{   
    public float rotationSpeed = 100f;
    public GameObject saw;

    private void Update()
    {
        saw.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
