using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int maxHealth = 50;
    public int currentHealth;
    public int damage = 10;

    private EnemyAnimatorController animatorController;

    private void Start()
    {
        currentHealth = maxHealth;
        animatorController = GetComponent<EnemyAnimatorController>();

        Debug.Log($"[Enemy] Health Initialized: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    //    Debug.Log($"[Enemy] Took {damageAmount} damage. Health: {currentHealth}/{maxHealth}");

        if (animatorController != null)
            animatorController.PlayTakeHit();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("[Enemy] Enemy has died.");
        // You can play a death animation, drop loot, destroy object, etc.
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }
}
