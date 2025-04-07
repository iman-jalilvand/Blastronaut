using UnityEngine;

public class Rocket : MonoBehaviour
{
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
            Bullet.FireBullet(bulletPrefab, bulletSpawnRef, ShootForce);
        }
    }
}
