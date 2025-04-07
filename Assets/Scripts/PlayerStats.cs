using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int strength = 10;
    public bool isBlocking = false;
    public int resilience = 5;

    private PlayerAnimatorController animController;

    void Start()
    {
        currentHealth = maxHealth;
        animController = GetComponent<PlayerAnimatorController>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isBlocking)
        {
            damageAmount -= resilience;
            damageAmount = Mathf.Max(0, damageAmount);
        }

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"[Player] Took {damageAmount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            animController.PlayDeath();
            Debug.Log("[Player] You died.");
        }
        else
        {
            animController.PlayHurt();
        }
    }

    public int GetAttackDamage()
    {
        return strength;
    }
}
