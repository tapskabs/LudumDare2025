using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Enviroment : MonoBehaviour
{
    [Header("Raycast Settings")]
    [Tooltip("How far down to check for floors")]
    public float floorCheckDistance = 1f;
    [Tooltip("Offset from object center to start raycast")]
    public float raycastOffset = 0.2f;
    [Tooltip("Which layers contain floor colliders")]
    public LayerMask floorLayers;

    [Header("Positioning")]
    [Tooltip("How high above floor to maintain position")]
    public float floorHeightOffset = 0.5f;
    [Tooltip("How quickly to snap to floor position")]
    public float snapSpeed = 10f;

    private Rigidbody rb;
    private Collider col;
    private bool wasOnFloorLastFrame;
    private Vector3 lastFloorPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // Disable gravity since we're handling positioning manually
        rb.useGravity = false;

        // Start by assuming current position is valid
        lastFloorPosition = transform.position;
    }

    private void FixedUpdate()
    {
        CheckFloorAndAdjust();
    }

    private void CheckFloorAndAdjust()
    {
        Vector3 rayStart = transform.position + Vector3.up * raycastOffset;
        bool isOnFloor = Physics.Raycast(rayStart, Vector3.down,
                        out RaycastHit hit, floorCheckDistance + raycastOffset, floorLayers);

        if (isOnFloor)
        {
            // Calculate target position above floor
            Vector3 targetPosition = new Vector3(
                transform.position.x,
                hit.point.y + floorHeightOffset,
                transform.position.z
            );

            // Smoothly move toward target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, snapSpeed * Time.fixedDeltaTime);

            // Reset velocity to prevent drifting
            rb.linearVelocity = Vector3.zero;

            // Remember this valid floor position
            lastFloorPosition = targetPosition;
            wasOnFloorLastFrame = true;
        }
        else if (wasOnFloorLastFrame)
        {
            // If we just left the floor, try to return to last good position
            transform.position = Vector3.Lerp(transform.position, lastFloorPosition, snapSpeed * Time.fixedDeltaTime);

            // If still not on floor after adjustment, keep looking
            if (!Physics.Raycast(transform.position + Vector3.up * raycastOffset,
                Vector3.down, floorCheckDistance + raycastOffset, floorLayers))
            {
                wasOnFloorLastFrame = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the floor check ray
        Gizmos.color = Color.cyan;
        Vector3 rayStart = transform.position + Vector3.up * raycastOffset;
        Gizmos.DrawLine(rayStart, rayStart + Vector3.down * (floorCheckDistance + raycastOffset));

        // Draw the floor height offset
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, floorCheckDistance + raycastOffset, floorLayers))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(new Vector3(
                transform.position.x,
                hit.point.y + floorHeightOffset,
                transform.position.z
            ), 0.2f);
        }
    }
}
