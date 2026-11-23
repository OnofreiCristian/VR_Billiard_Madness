using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Explosion Settings")]
    [Tooltip("How many seconds after spawning/grabbing before it explodes.")]
    public float fuseTime = 3f;

    [Tooltip("How wide the blast reaches (in meters).")]
    public float explosionRadius = 1.5f;

    [Tooltip("How hard the balls get launched.")]
    public float explosionForce = 15f;

    [Tooltip("Adds a slight upward pop so balls jump into the air instead of just sliding.")]
    public float upwardsModifier = 0.5f;

    private bool hasExploded = false;

    private void Start()
    {
        Invoke(nameof(Explode), fuseTime);
    }

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hitColliders)
        {
            if (!hit.CompareTag("PoolBall")) continue;

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(
                    explosionForce,
                    transform.position,
                    explosionRadius,
                    upwardsModifier,
                    ForceMode.Impulse
                );
            }
        }

        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}