using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionParticle;

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
    /// Instantiates a missile and sets its velocity relative to the rocket's speed
    /// </summary>
    public static void FireMissile(GameObject missile, Transform spawnPoint, float bulletSpeed)
    {
        // Get rocket reference from spawnPoint root
        Rigidbody rocketRb = spawnPoint.root.GetComponent<Rigidbody>();

        // Instantiate the missile
        GameObject MISSILE = Instantiate(missile, spawnPoint.position, spawnPoint.rotation);

        // Apply velocity to the missile: rocket velocity + forward shot force
        Rigidbody bulletRb = MISSILE.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = rocketRb.linearVelocity + spawnPoint.forward * bulletSpeed;

        // Prevent collision between rocket and missile
        Collider rocketCol = rocketRb.GetComponent<Collider>();
        Collider bulletCol = MISSILE.GetComponent<Collider>();
        if (rocketCol != null && bulletCol != null)
        {
            Physics.IgnoreCollision(rocketCol, bulletCol);
        }
        // Play the missile's own 3D audio
        AudioSource missileAudio = MISSILE.GetComponent<AudioSource>();
        if (missileAudio != null)
            missileAudio.Play();

        // Auto destroy bullet after 4 seconds
        Destroy(MISSILE, 4f);
    }
}