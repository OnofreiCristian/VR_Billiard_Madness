using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fuseTime = 3f;
    public float explosionRadius = 1.5f;
    public float explosionForce = 15f;
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