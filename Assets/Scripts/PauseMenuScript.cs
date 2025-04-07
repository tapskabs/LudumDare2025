using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] Canvas pauseMenu;
    [SerializeField] TMP_Text Health;
    [SerializeField] TMP_Text Strength;
    [SerializeField] TMP_Text Resilience;
    [SerializeField] string currentSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pauseMenu.enabled = true;
            Time.timeScale = 0.0f;

            UpdateStats();

        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            pauseMenu.enabled = false;
            Time.timeScale = 1.0f;
        }

    }


    private void UpdateStats()
    {
        Health.text = PlayerStats.Instance.currentHealth.ToString();
        Strength.text = PlayerStats.Instance.strength.ToString();
        Resilience.text = PlayerStats.Instance.resilience.ToString();
    }

    public void SwitchMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(currentSceneName);
    }
}
