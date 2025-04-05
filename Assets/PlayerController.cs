using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Sprite")]
    public Transform spriteTransform; // The sprite object (child of player)
    public bool faceCamera = true;    // Set true to always face the camera

    private Rigidbody rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // Flip sprite based on movement direction (X only)
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0 && spriteTransform != null)
        {
            spriteTransform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);
        }

        // Optional: Make sprite face the camera
        if (faceCamera && Camera.main != null && spriteTransform != null)
        {
            Vector3 lookDir = Camera.main.transform.forward;
            lookDir.y = 0;
            spriteTransform.forward = lookDir;
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized * moveSpeed;
        Vector3 velocity = new Vector3(move.x, rb.velocity.y, move.z);
        rb.velocity = velocity;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
