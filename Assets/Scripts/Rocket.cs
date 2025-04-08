using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject normalMissilePrefab;
    public GameObject homingMissilePrefab;
    public TargetingSystem targetingSystem;
    private enum MissileType { Normal, Homing }
    private MissileType currentMissileType = MissileType.Normal;


    public float MoveForce;
    public float TurnTorque;
    public Rigidbody rocket;
    public Transform bulletSpawnRef;
    public GameObject bulletPrefab;
    public float ShootForce;
    public GameObject flame;

    private bool isMoving = false; // To check if the rocket is moving

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isMoving = false; // Reset movement tracker

        if (InputManager.Instance.MoveUp)
        {
            rocket.AddForce(transform.forward * MoveForce);
            isMoving = true;
        }

        if (InputManager.Instance.MoveDown)
        {
            rocket.AddForce(-transform.forward * MoveForce);
            isMoving = true;
        }

        if (InputManager.Instance.MoveRight)
        {
            rocket.AddForce(transform.right * MoveForce);
            isMoving = true;
        }

        if (InputManager.Instance.MoveLeft)
        {
            rocket.AddForce(-transform.right * MoveForce);
            isMoving = true;
        }

        // Enable flame effect when moving
        if (flame != null)
        {
            flame.SetActive(isMoving);
        }

        if (isMoving)
        {
            AudioManager.Instance?.PlayMovementSound();
        }
        else
        {
            AudioManager.Instance?.StopMovementSound();
        }

        if (InputManager.Instance.IsRotating)
        {
            rocket.AddRelativeTorque(
                -InputManager.Instance.VerticalLook * TurnTorque,
                InputManager.Instance.HorizontalLook * TurnTorque,0);
        }

        if (PauseMenu.isPaused) return; // Prevent shooting while paused

        
        if (InputManager.Instance.IsShooting)
        {
            if (currentMissileType == MissileType.Normal)
            {
                Bullet.FireBullet(normalMissilePrefab, bulletSpawnRef, ShootForce);
            }
            else if (currentMissileType == MissileType.Homing)
            {
                GameObject target = targetingSystem.GetLockedTarget();
                if (target != null)
                {
                    GameObject missile = Instantiate(homingMissilePrefab, bulletSpawnRef.position, bulletSpawnRef.rotation);
                    missile.GetComponent<HomingMissile>().SetTarget(target.transform);
                }
            }
        }

        // Switch between normal and homing missiles
        if (InputManager.Instance.SwitchWeapon)
        {
            currentMissileType = (currentMissileType == MissileType.Normal) ? MissileType.Homing : MissileType.Normal;
            Debug.Log("Switched to: " + currentMissileType);
        }

    }
}
