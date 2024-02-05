using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClearPlayerPrefs()
    {
       // Clear all player preferences
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Startscreen");
    }
        
}
