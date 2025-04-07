using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Player UI")]
    public Slider playerHealthSlider;
    public TextMeshProUGUI playerStatsText;

    [Header("Enemy UI")]
    public List<EnemyUI> enemyUIs = new List<EnemyUI>();

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats != null)
        {
            playerHealthSlider.maxValue = playerStats.maxHealth;
        }

        foreach (EnemyUI enemyUI in enemyUIs)
        {
            if (enemyUI.enemyStats != null && enemyUI.enemySlider != null)
            {
                enemyUI.enemySlider.maxValue = enemyUI.enemyStats.maxHealth;
            }
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Update player UI
        if (playerStats != null)
        {
            playerHealthSlider.value = playerStats.currentHealth;
            playerStatsText.text = $"HP: {playerStats.currentHealth}/{playerStats.maxHealth}\n" +
                                   $"STR: {playerStats.strength}\n" +
                                   $"RES: {playerStats.resilience}";
        }

        // Update enemy UI
        foreach (EnemyUI enemyUI in enemyUIs)
        {
            if (enemyUI.enemyStats != null && enemyUI.enemySlider != null)
            {
                enemyUI.enemySlider.value = enemyUI.enemyStats.currentHealth;
            }
        }
    }
}

[System.Serializable]
public class EnemyUI
{
    public EnemyStats enemyStats;
    public Slider enemySlider;
}