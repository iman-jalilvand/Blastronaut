using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Rocket rocket; // Reference to the Rocket script

    public float lockTime = 3f;
    [SerializeField] private float lockAngle = 12f;   // Smaller cone, like 150px width
    [SerializeField] private float lockDistance = 50f;


    private float lockTimer = 0f;
    private GameObject lockedTarget;
    private GameObject currentTarget;

    void Update()
    {
        if (!rocket.IsHomingMode()) return; // ✅ Check missile mode

            currentTarget = FindAsteroidInFront();

            if (currentTarget != null)
            {
                if (currentTarget == lockedTarget)
                {
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