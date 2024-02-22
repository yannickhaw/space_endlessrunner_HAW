using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public void Continue()                                  // wird aufgerufen, wenn der Continue Button geklickt wird
    {
        PauseManager.gamePaused  = false;                    // Setzt das Spiel fort
        Time.timeScale = 1;                                 // Setzt die Zeit wieder auf normal 
    }

    public void EndGame()                                // wird aufgerufen, wenn der Home/End_Run-Button gedrückt wird
    {
        PauseManager.gamePaused  = false;               // Deaktiviert den Pause Status
        Time.timeScale = 1;                              // Setzt die Zeit wieder auf normal 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);   // Lädt die vorherige Szene (Startbildschirm) und beendet damit das Spiel
    }

    public void PauseGame()                                 // wird aufgerufen, wenn der Pause-Button geklickt wird
    {
        PauseManager.gamePaused  = true;                    // Pausiert das Spiel
    }
}
