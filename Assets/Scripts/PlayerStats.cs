using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Core stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int strength = 10;
    public int resilience = 5;

    [Header("Blocking")]
    public bool isBlocking = false;
    [UnityEngine.Range(0f, 1f)] public float blockDamageReduction = 0.5f;

    private int[] statArray = new int[3];

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"[Player] Health Initialized: {currentHealth}/{maxHealth}");

        statArray[0] = maxHealth;
        statArray[1] = strength;
        statArray[2] = resilience;

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

    public void IncreaseStat(int statRange)
    {
        int increase = Random.Range(2, statRange);
        maxHealth += increase;

        increase = Random.Range(2, statRange);
        strength += increase;

        increase = Random.Range(2, statRange);
        resilience += increase;

    }
}
