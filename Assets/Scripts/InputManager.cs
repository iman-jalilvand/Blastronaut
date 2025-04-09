using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance; // Singleton instance

    public bool MoveUp { get; private set; }
    public bool MoveDown { get; private set; }
    public bool MoveRight { get; private set; }
    public bool MoveLeft { get; private set; }
    public float HorizontalLook { get; private set; }
    public float VerticalLook { get; private set; }
    public bool IsRotating { get; private set; }
    public bool IsShooting { get; private set; }
    public bool SwitchWeapon { get; private set; }

    private void Awake()
    {
        Instance = this; // Setup Singleton
    }

    private void Update()
    {
        // Movement input (WASD keys)
        MoveUp = Input.GetKey(KeyCode.W);
        MoveDown = Input.GetKey(KeyCode.S);
        MoveRight = Input.GetKey(KeyCode.D);
        MoveLeft = Input.GetKey(KeyCode.A);

        // Mouse movement for looking/rotation
        HorizontalLook = Input.GetAxis("Mouse X");
        VerticalLook = Input.GetAxis("Mouse Y");
        IsRotating = Input.GetMouseButton(1); // Right-click to rotate

        // Shooting input (Left mouse button)
        IsShooting = Input.GetMouseButtonDown(0);
        SwitchWeapon = Input.GetKeyDown(KeyCode.E);
    }
}
