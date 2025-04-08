using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public RectTransform targetBox;
    public GameObject lockBoxPrefab;
    public Canvas canvas;
    public Camera mainCam;
    public float lockTime = 3f;

    private float lockTimer = 0f;
    private GameObject lockedTarget;
    private GameObject currentTarget;
    private GameObject lockBoxInstance;

    void Update()
    {
        currentTarget = FindAsteroidInTargetBox();

        if (currentTarget != null)
        {
            if (currentTarget == lockedTarget)
            {
                UpdateLockUI();
            }
            else
            {
                lockTimer += Time.deltaTime;
                if (lockTimer >= lockTime)
                {
                    LockOn(currentTarget);
                }
            }
        }
        else
        {
            ClearLock();
        }
    }

    GameObject FindAsteroidInTargetBox()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids)
        {
            Vector3 screenPos = mainCam.WorldToScreenPoint(asteroid.transform.position);
            if (RectTransformUtility.RectangleContainsScreenPoint(targetBox, screenPos, mainCam))
                return asteroid;
        }
        return null;
    }

    void LockOn(GameObject target)
    {
        lockedTarget = target;
        lockTimer = 0f;

        if (lockBoxInstance == null)
            lockBoxInstance = Instantiate(lockBoxPrefab, canvas.transform);
    }

    void UpdateLockUI()
    {
        if (lockBoxInstance != null && lockedTarget != null)
        {
            Vector3 screenPos = mainCam.WorldToScreenPoint(lockedTarget.transform.position);
            lockBoxInstance.GetComponent<RectTransform>().position = screenPos;
        }
    }

    void ClearLock()
    {
        lockTimer = 0f;
        lockedTarget = null;
        if (lockBoxInstance != null)
        {
            Destroy(lockBoxInstance);
        }
    }

    public GameObject GetLockedTarget()
    {
        return lockedTarget;
    }
}
