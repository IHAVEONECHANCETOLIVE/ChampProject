using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    public Vector2 movementInput;
    private float force = 100f;
    private bool jumpPressed;
    public Rigidbody rb;

    public float jumpForce = 5f;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = GetComponent<InputSystem_Actions>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += x => movementInput = x.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += x => movementInput = Vector2.zero;
            inputActions.Player.Jump.performed += x => jumpPressed = true;
            inputActions.Player.Jump.canceled += x => jumpPressed = false;
        }
    }
    void OnDisable()
    {
        if (inputActions != null)
            inputActions.Player.Disable();
    }
    void FixedUpdate()
    {
        // Проверка на землю
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpPressed = false; // чтобы прыгать только один раз на нажатие
        }
    }
}
