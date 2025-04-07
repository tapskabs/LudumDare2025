using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerStats playerStats;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        // Handle grounded/air
        animator.SetBool("Grounded", IsGrounded()); 
        animator.SetFloat("AirSpeed", GetComponent<Rigidbody>().linearVelocity.y);

        // Set movement state
        float speed = new Vector2(GetComponent<Rigidbody>().linearVelocity.x, GetComponent<Rigidbody>().linearVelocity.z).magnitude;
        animator.SetInteger("AnimState", speed > 0.1f ? 1 : 0); // 0 = Idle, 1 = Run

       
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayJump()
    {
        animator.SetTrigger("Jump");
    }

    public void PlayHurt()
    {
        animator.SetTrigger("Hurt");
    }

    public void PlayRecover()
    {
        animator.SetTrigger("Recover");
    }

    public void PlayDeath()
    {
        animator.SetTrigger("Death");
    }

    private bool IsGrounded()
    {
        // Simple grounded check � replace with your actual implementation
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
