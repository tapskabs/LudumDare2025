using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    [Header("Strength")]
    public int strength = 10;        // Damage output
    [Header("Resilience")]
    public int resilience = 5;       // Damage reduction

    [Header("Blocking")]
    public bool isBlocking = false;
    [Range(0f, 1f)] public float blockDamageReduction = 0.5f; // 50% damage reduction when blocking

    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Call this to damage the player
    /// </summary>
    public void TakeDamage(int damage)
    {
        int finalDamage = damage - resilience;

        if (isBlocking)
        {
            finalDamage = Mathf.RoundToInt(finalDamage * (1f - blockDamageReduction));
        }

        finalDamage = Mathf.Clamp(finalDamage, 0, int.MaxValue);
        currentHealth -= finalDamage;

        Debug.Log($"Player took {finalDamage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Heal the player
    /// </summary>
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Player healed {amount}. Health: {currentHealth}/{maxHealth}");
    }

    /// <summary>
    /// Use this to toggle blocking on/off
    /// </summary>
    public void SetBlocking(bool state)
    {
        isBlocking = state;
        Debug.Log($"Blocking: {isBlocking}");
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Trigger death animation, disable controls, etc.
    }

    /// <summary>
    /// Call to deal damage to enemies using strength
    /// </summary>
    public int GetAttackDamage()
    {
        return strength;
    }
}
