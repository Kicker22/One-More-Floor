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
        // Get animator from child if not on this GameObject
        animator = GetComponentInChildren<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
        else
        {
            Debug.Log("Animator found on " + animator.gameObject.name);
        }
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
            // Move the character
            Vector3 move = moveDirection.normalized * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
            
            // Smoothly rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);
            
            // Update animator for walking/running animation
            if (animator != null)
            {
                animator.SetFloat("Speed", inputVector.magnitude);
                Debug.Log("Setting Speed to: " + inputVector.magnitude);
            }
        }
        else
        {
            // Player is idle - set speed to 0
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    // Called by animation events for footstep sounds
    void OnFootstep()
    {
        // TODO: Add footstep sound effect here
        // Example: AudioSource.PlayClipAtPoint(footstepSound, transform.position);
    }
}
