using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;
    public TextMeshProUGUI playerStatsText;

    private PlayerStats playerStats;
    private EnemyStats enemyStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemyStats = FindObjectOfType<EnemyStats>();

        // Set slider max values
        playerHealthSlider.maxValue = playerStats.maxHealth;
        enemyHealthSlider.maxValue = enemyStats.maxHealth;

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Update sliders
        playerHealthSlider.value = playerStats.currentHealth;
        enemyHealthSlider.value = enemyStats.currentHealth;

        // Update text
        playerStatsText.text = $"HP: {playerStats.currentHealth}/{playerStats.maxHealth}\n" +
                               $"STR: {playerStats.strength}\n" +
                               $"RES: {playerStats.resilience}";
    }
}
