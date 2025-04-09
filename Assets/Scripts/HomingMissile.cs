using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 500f; // Degrees per second
    [SerializeField] private float detectionRadius = 0.1f; // Explode when close enough

    private Transform target;
    public GameObject explosionEffect;

    void Start()
    {
        // Play missile engine sound (looping, 3D)
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && !audio.isPlaying)
        {
            audio.Play();
        }
    }

    
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Direction toward the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Rotate smoothly toward the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        // Move forward constantly
        transform.position += transform.forward * speed * Time.deltaTime;

        // Check if we're close enough to auto-detonate
        if (Vector3.Distance(transform.position, target.position) <= detectionRadius)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null && other.transform == target)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Play explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Play explosion sound via AudioManager (same as normal missile)
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.explosionClip);
        }

        // Ask the asteroid to destroy itself and award score
        if (target != null && target.CompareTag("Asteroid"))
        {
            Asteroid asteroid = target.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                asteroid.TakeHit(); // New method we'll create in Asteroid.cs
            }
        }

        // Destroy the missile itself
        Destroy(gameObject);
    }


}
