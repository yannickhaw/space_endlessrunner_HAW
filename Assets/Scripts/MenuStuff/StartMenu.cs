using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()                         // wird aufgerufen, wenn der "PLAY"-Button gedrückt wird
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        // Startet das Spiel
    }

    public void QuitGame()                          // Wird aufgerufen, wenn der "Exit Game" Button gedrückt wird
    {
        Application.Quit();                         // Beendet das Programm/Anwendung
    }

    public void ClearPlayerPrefs()                  // Wird aufgerufen, wenn der "Clear Prefs" Button gedrückt wird
    {
        PlayerPrefs.DeleteAll();                    // Löscht alle Spielerpräferenzen
        SceneManager.LoadScene("Startscreen");      // Lädt Startszene neu
    }
        
}
