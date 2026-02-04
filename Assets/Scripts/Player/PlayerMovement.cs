using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // This script will handle player movement in the game.
    // Third-person movement with smooth rotation towards movement direction
    [Tooltip("Speed at which the player moves")]
    public float speed = 5f;
    [Tooltip("Rotation speed for turning towards movement direction")]
    public float rotationSpeed = 10f;
    [Tooltip("Reference to the Rigidbody component for physics calculations")]
    public Rigidbody rb;
    [Tooltip("Reference to unity input system for player controls")]
    InputAction moveAction;
    
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // Third-person movement: WASD moves in world directions
        // Character smoothly rotates to face the movement direction
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        
        // Calculate movement direction in world space
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        
        if (moveDirection.magnitude >= 0.1f)
        {
            // Move the character using velocity for more responsive movement
            Vector3 targetVelocity = moveDirection.normalized * speed;
            targetVelocity.y = rb.linearVelocity.y; // Preserve vertical velocity for gravity
            rb.linearVelocity = targetVelocity;
            
            // Smoothly rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);
            
            // Update animator speed parameter
            if (animator != null)
            {
                animator.SetFloat("Speed", inputVector.magnitude);
            }
        }
        else
        {
            // Stop horizontal movement when no input
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            
            // Player is idle
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }
}
