using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    //Just need something to change the scene

    public void BackToStart()
    {
        SceneManager.LoadSceneAsync("mainMenu");
    }
}
