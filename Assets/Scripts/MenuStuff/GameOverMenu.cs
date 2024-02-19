using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()                               // wird aufgerufen, wenn der Restart Button geklcikt wird
    {
        SceneManager.LoadScene("Space_Runner_Scene");       // Lädt die Szene neu (Spiel wird neu gestartet)
    }

    public void HomeScreen()                                // wird aufgerufen, wenn der Home-Button gedrückt wird
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);   // Lädt die vorherige Szene (Startbildschirm)
    }
}
