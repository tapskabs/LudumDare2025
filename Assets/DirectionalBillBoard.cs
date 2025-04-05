using UnityEngine;

public class DirectionalBillBoard : MonoBehaviour
{
    private Camera mainCamera;
    private bool isFacingRight = true;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0; // Keep sprite upright

        // Face the camera but allow left/right flips
        transform.forward = cameraForward;

        // Optional: Flip sprite based on movement (like 2D)
        float moveDir = Input.GetAxis("Horizontal");
        if (moveDir > 0 && !isFacingRight) FlipSprite();
        else if (moveDir < 0 && isFacingRight) FlipSprite();
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flip horizontally
        transform.localScale = scale;
    }
}
