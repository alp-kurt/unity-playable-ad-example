using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Joystick Input")]
    public Joystick joystick;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;

    [Header("References")]
    public Transform cameraTransform; // Reference to the camera

    private Rigidbody rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent Rigidbody from affecting rotation
        animator = GetComponent<Animator>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Automatically assign main camera if not set
        }
    }

    private void FixedUpdate()
    {
        // Get input direction from joystick
        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        // Convert to world space based on camera orientation
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Flatten the camera vectors to ignore vertical tilt
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction relative to the camera
        Vector3 worldMoveDirection = (cameraRight * moveDirection.x + cameraForward * moveDirection.z).normalized;

        // Move Character
        if (worldMoveDirection.magnitude > 0.1f)
        {
            Vector3 targetPosition = rb.position + worldMoveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            // Rotate Character
            Quaternion toRotation = Quaternion.LookRotation(worldMoveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Play animation
        if (animator != null)
        {
            animator.SetFloat("Speed", moveDirection.magnitude);
        }
    }
}
