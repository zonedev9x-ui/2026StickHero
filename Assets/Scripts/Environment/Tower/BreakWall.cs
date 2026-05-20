using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //rb.AddForce(transform.forward * 1f, ForceMode.Impulse);
    }
}

