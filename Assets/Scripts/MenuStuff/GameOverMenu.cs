using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Space_Runner_Scene");
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
