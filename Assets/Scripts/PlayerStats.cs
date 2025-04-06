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

    private Animator m_animator;

    private void Start()
    {
        currentHealth = maxHealth;
        m_animator = GetComponent<Animator>();
        Debug.Log($"[Player] Health Initialized: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(int damage)
    {
        int finalDamage = damage - resilience;

        if (isBlocking)
        {
            finalDamage = Mathf.RoundToInt(finalDamage * (1f - blockDamageReduction));
            Debug.Log($"[Player] Blocking! Raw: {damage}, Reduced by {resilience}, Block Reduced to: {finalDamage}");
            m_animator?.SetTrigger("BlockImpact");
        }
        else
        {
            Debug.Log($"[Player] Hit! Raw: {damage}, Reduced by {resilience}, Final: {finalDamage}");
            m_animator?.SetTrigger("Hurt");
        }

        finalDamage = Mathf.Clamp(finalDamage, 0, int.MaxValue);
        currentHealth -= finalDamage;

        Debug.Log($"[Player] Took {finalDamage} damage. Current Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Overloaded version to handle float damage (without throwing exception)
    public void TakeDamage(float damage)
    {
        TakeDamage(Mathf.RoundToInt(damage));
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"[Player] Healed {amount}. Current Health: {currentHealth}/{maxHealth}");
        m_animator?.SetTrigger("Heal");
    }

    public void SetBlocking(bool state)
    {
        isBlocking = state;
        m_animator?.SetBool("IsBlocking", state);
        Debug.Log($"[Player] Blocking state set to: {isBlocking}");
    }

    private void Die()
    {
        Debug.Log("[Player] You have died.");
        m_animator?.SetTrigger("Death");
        // Add additional death handling here
    }

    public int GetAttackDamage()
    {
        return strength;
    }
}
