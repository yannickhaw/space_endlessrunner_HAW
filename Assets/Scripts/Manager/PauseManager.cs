using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool gamePaused;            // Statische Variable, die den Spielzustand speichert (ob das Spiel vorbei ist oder nicht)
    public GameObject pausePanel;        // GameObject des Game Over-Panels (Game Over Screen (UI) nach Tod des Spielers)
    public GameObject pauseButton;      // GameObject des Pause Buttons (UI)
  
    
    void Start()                            // Start wird beim ersten Frame aufgerufen
    {
        gamePaused = false;                   // Setzt den Spielzustand auf "nicht vorbei" beim Start
    }

    void Update()                           // Update wird einmal pro Frame aufgerufen
    {
        if (gamePaused)                       // Überprüft, ob das Spiel vorbei ist
        {
            Time.timeScale = 0;                 // Setzt die Zeit auf Null, damit das Spiel pausiert wird
            pausePanel.SetActive(true);         // Aktiviert das Pause-Panel, um es anzuzeigen (UI)    
            pauseButton.SetActive(false);        // Dektiviert den Pause-Button, um ihn nicht mehr anzuzeigen (UI) 

        }
        else if (!GameOverManager.gameOver)
        {
            pausePanel.SetActive(false);         // Dektiviert das Pause-Panel, um es  nicht mehr anzuzeigen (UI)    
            pauseButton.SetActive(true);        // Aktiviert den Pause-Button, um ihn anzuzeigen (UI) 
        }
    }
}
