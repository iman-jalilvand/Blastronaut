using UnityEngine;
using UnityEngine.UI;

public class TargetingSystem : MonoBehaviour
{
    public Rocket rocket; // Reference to the Rocket script

    public float lockTime = 3f;
    [SerializeField] private float lockAngle = 5f;   // Cone of lockable targets
    [SerializeField] private float lockDistance = 40f;

    private float lockTimer = 0f;
    private GameObject lockedTarget;
    private GameObject currentTarget;

    // Red lock box
    public GameObject lockBoxPrefab; // Red hollow box prefab
    public Canvas canvas;            // Reference to UI canvas
    public AudioManager audioManager;

    private GameObject lockBoxInstance;

    // Blinking red box
    private float blinkTimer = 0f;
    private float blinkInterval = 0.3f; // Blink speed
    private bool hasPlayedBeep = false;


    void Update()
    {
        // Always update red box to follow target
        if (lockedTarget != null)
        {
            UpdateLockUI();
        }

        // Only operate targeting in homing mode
        if (!rocket.IsHomingMode())
        {
            // Cancel everything if homing mode is OFF
            if (audioManager != null)
                audioManager.StopLockOnSound();

            ClearLock(); // Destroys red box, resets timers, etc.
            return;
        }

        currentTarget = FindAsteroidInFront();

        if (currentTarget != null)
        {
            if (currentTarget == lockedTarget)
                return;

            if (!hasPlayedBeep && audioManager != null)
            {
                audioManager.PlayLockOnSound();
                hasPlayedBeep = true;
            }


            lockTimer += Time.deltaTime;

            // 🔴 Instantiate red lock box if not already created
            if (lockBoxInstance == null)
            {
                lockBoxInstance = Instantiate(lockBoxPrefab, canvas.transform);
            }

            // 🔁 Blinking behavior during lock countdown
            blinkTimer += Time.deltaTime;
            if (lockBoxInstance != null)
            {
                bool shouldBeVisible = Mathf.FloorToInt(blinkTimer / blinkInterval) % 2 == 0;
                lockBoxInstance.SetActive(shouldBeVisible);

                Vector3 screenPos = Camera.main.WorldToScreenPoint(currentTarget.transform.position);
                lockBoxInstance.GetComponent<RectTransform>().position = screenPos;
            }

            // ✅ Lock when time reached
            if (lockTimer >= lockTime)
            {
                LockOn(currentTarget);
            }
        }
        else
        {
            // ❌ Target lost – stop lock-on sound
            if (audioManager != null)
            {
                audioManager.StopLockOnSound();
            }
            ClearLock();        
        }
    }

    GameObject FindAsteroidInFront()
    {
        GameObject[] normalAsteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        GameObject[] magneticAsteroids = GameObject.FindGameObjectsWithTag("MagneticAsteroid");

        GameObject[] allAsteroids = new GameObject[normalAsteroids.Length + magneticAsteroids.Length];
        normalAsteroids.CopyTo(allAsteroids, 0);
        magneticAsteroids.CopyTo(allAsteroids, normalAsteroids.Length);

        GameObject best = null;
        float closestAngle = lockAngle;

        foreach (var asteroid in allAsteroids)
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
        blinkTimer = 0f;

        if (lockBoxInstance == null)
        {
            lockBoxInstance = Instantiate(lockBoxPrefab, canvas.transform);
        }

        // Stop blinking and make box solid
        if (lockBoxInstance != null)
        {
            lockBoxInstance.SetActive(true);
            UpdateLockUI();
        }

        Debug.Log("✅ Locked on: " + target.name);
    }

    void UpdateLockUI()
    {
        if (lockBoxInstance != null && lockedTarget != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(lockedTarget.transform.position);
            lockBoxInstance.GetComponent<RectTransform>().position = screenPos;
        }
    }

    void ClearLock()
    {
        hasPlayedBeep = false;

        lockedTarget = null;
        lockTimer = 0f;
        blinkTimer = 0f;

        if (lockBoxInstance != null)
        {
            Destroy(lockBoxInstance);
            lockBoxInstance = null;
        }
    }

    public GameObject GetLockedTarget()
    {
        return lockedTarget;
    }
}
