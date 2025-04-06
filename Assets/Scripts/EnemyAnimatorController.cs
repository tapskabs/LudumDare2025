using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Vector2 lastPosition;

    public float walkThreshold = 0.01f; // Minimum movement to count as walking

    private void Awake()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector2 currentPosition = transform.position;
        float distanceMoved = Vector2.Distance(currentPosition, lastPosition);

        bool isWalking = distanceMoved > walkThreshold;
        animator.SetBool("isWalking", isWalking);

        lastPosition = currentPosition;
    }

    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }

    public void PlayTakeHit()
    {
        animator.SetTrigger("takeHit");
    }
}
