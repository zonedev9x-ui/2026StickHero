using UnityEngine;

public class BreakWall : MonoBehaviour
{
    public float explosionForce = 800f;
    public float explosionRadius = 5f;
    public float upwardsModifier = 1f;

    private Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.AddExplosionForce(
            explosionForce,
            transform.position,
            explosionRadius,
            upwardsModifier,
            ForceMode.Impulse
        );
    }
}

