using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
     public float lockTime = 3f;
    public float lockAngle = 30f;
    public float lockDistance = 100f;

    private float lockTimer = 0f;
    private GameObject lockedTarget;
    private GameObject currentTarget;

    void Update()
    {
        currentTarget = FindAsteroidInFront();

        if (currentTarget != null)
        {
            if (currentTarget == lockedTarget)
            {
                // Already locked
                return;
            }

            lockTimer += Time.deltaTime;
            if (lockTimer >= lockTime)
            {
                LockOn(currentTarget);
            }
        }
        else
        {
            ClearLock();
        }
    }

    GameObject FindAsteroidInFront()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject best = null;
        float closestAngle = lockAngle;

        foreach (var asteroid in asteroids)
        {
            Vector3 dir = asteroid.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, dir);

            if (angle < closestAngle && dir.magnitude < lockDistance)
            {
                closestAngle = angle;
                best = asteroid;
            }
        }

        return best;
    }

    void LockOn(GameObject target)
    {
        lockedTarget = target;
        lockTimer = 0f;
        Debug.Log("✅ Locked on: " + target.name);
    }

    void ClearLock()
    {
        lockedTarget = null;
        lockTimer = 0f;
    }

    public GameObject GetLockedTarget()
    {
        return lockedTarget;
    }
}