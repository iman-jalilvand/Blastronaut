using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 15f;
    public float rotateSpeed = 720f; // Degrees per second
    [SerializeField] private float detectionRadius = 2f; // Explode when close enough

    private Transform target;
    public GameObject explosionEffect;

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

    void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
