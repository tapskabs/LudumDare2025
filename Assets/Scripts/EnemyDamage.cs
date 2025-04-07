using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f;
    private float lastDamageTime;

    private EnemyAnimatorController animatorController;

    private void Start()
    {
        animatorController = GetComponent<EnemyAnimatorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TryDamagePlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDamagePlayer(other);
    }

    private void TryDamagePlayer(Collider other)
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return;

        PlayerStats player = other.GetComponent<PlayerStats>();
        if (player != null)
        {
            player.TakeDamage(damage);
           //Debug.Log($"[Enemy] Attacked player for {damage} damage.");

            // Trigger attack animation
            if (animatorController != null)
            {
                animatorController.SetAttacking(true);
                Invoke(nameof(ResetAttackAnimation), 0.2f); // Reset shortly after
            }

            lastDamageTime = Time.time;
        }
    }

    private void ResetAttackAnimation()
    {
        if (animatorController != null)
            animatorController.SetAttacking(false);
    }
}
