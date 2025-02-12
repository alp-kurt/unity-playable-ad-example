using UnityEngine;
using Luna.Unity;

public class CharacterController : MonoBehaviour
{
    [Header("Joystick Input")]
    public Joystick joystick;

    [Header("Movement Settings")]
    [LunaPlaygroundField("Character Speed", 1, "Gameplay Settings")]
    public float moveSpeed = 3f;

    public float rotationSpeed = 10f;

    [Header("Movement Boundaries")]
    public Vector3 minBounds = new Vector3(-5f, 0f, -5f); // Minimum XYZ values
    public Vector3 maxBounds = new Vector3(5f, 0f, 5f);   // Maximum XYZ values

    [Header("References")]
    public Transform cameraTransform;
    private Rigidbody rb;
    private Animator animator;
    private string currentAnimation = "";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            MoveCharacter(moveDirection);
            SetAnimation("Walk");
        }
        else
        {
            SetAnimation("Idle");
        }
    }

    private void MoveCharacter(Vector3 moveDirection)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 worldMoveDirection = (cameraRight * moveDirection.x + cameraForward * moveDirection.z).normalized;

        if (worldMoveDirection.magnitude > 0.1f)
        {
            // Calculate the new position
            Vector3 newPosition = rb.position + worldMoveDirection * moveSpeed * Time.fixedDeltaTime;

            // Clamp position within the defined boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
            newPosition.z = Mathf.Clamp(newPosition.z, minBounds.z, maxBounds.z);

            // Apply movement
            rb.MovePosition(newPosition);

            // Rotate Character
            Quaternion toRotation = Quaternion.LookRotation(worldMoveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Optimized function to switch animations only when needed
    /// </summary>
    private void SetAnimation(string newAnimation)
    {
        if (currentAnimation != newAnimation) // Prevent redundant animation calls
        {
            animator.Play(newAnimation);
            currentAnimation = newAnimation;
        }
    }
}
