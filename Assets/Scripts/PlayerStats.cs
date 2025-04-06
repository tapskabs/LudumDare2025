using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Core Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int strength = 10;
    public int resilience = 5;

    [Header("Blocking")]
    public bool isBlocking = false;
    [Range(0f, 1f)] public float blockDamageReduction = 0.5f;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"[Player] Health Initialized: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = damage - resilience;

        if (isBlocking)
        {
            finalDamage = Mathf.RoundToInt(finalDamage * (1f - blockDamageReduction));
            Debug.Log($"[Player] Blocking! Raw: {damage}, Reduced by {resilience}, Block Reduced to: {finalDamage}");
        }
        else
        {
            Debug.Log($"[Player] Hit! Raw: {damage}, Reduced by {resilience}, Final: {finalDamage}");
        }

        finalDamage = Mathf.Clamp(finalDamage, 0, int.MaxValue);
        currentHealth -= finalDamage;

        Debug.Log($"[Player] Took {finalDamage} damage. Current Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"[Player] Healed {amount}. Current Health: {currentHealth}/{maxHealth}");
    }

    public void SetBlocking(bool state)
    {
        isBlocking = state;
        Debug.Log($"[Player] Blocking state set to: {isBlocking}");
    }

    private void Die()
    {
        Debug.Log("[Player]  You have died.");
        // Trigger death animation, etc.
    }

    public int GetAttackDamage()
    {
        return strength;
    }

    internal void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}
