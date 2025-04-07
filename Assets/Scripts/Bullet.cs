using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionParticle;

    private void Start()
    {
        // Play bullet shooting sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.shootClip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Instantiate explosion effect
        if (explosionParticle != null)
        {
            Instantiate(explosionParticle, transform.position, transform.rotation);
        }

        // Play explosion sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.explosionClip);
        }

        // Destroy the bullet
        Destroy(gameObject);

        // Destroy asteroid if hit
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Instantiates a bullet and sets its velocity relative to the rocket's speed
    /// </summary>
    public static void FireBullet(GameObject bulletPrefab, Transform spawnPoint, float bulletSpeed)
    {
        // Get rocket reference from spawnPoint root
        Rigidbody rocketRb = spawnPoint.root.GetComponent<Rigidbody>();

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Apply velocity to the bullet: rocket velocity + forward shot force
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = rocketRb.linearVelocity + spawnPoint.forward * bulletSpeed;

        // Prevent collision between rocket and bullet
        Collider rocketCol = rocketRb.GetComponent<Collider>();
        Collider bulletCol = bullet.GetComponent<Collider>();
        if (rocketCol != null && bulletCol != null)
        {
            Physics.IgnoreCollision(rocketCol, bulletCol);
        }

        // Auto destroy bullet after 4 seconds
        Destroy(bullet, 4f);
    }
}