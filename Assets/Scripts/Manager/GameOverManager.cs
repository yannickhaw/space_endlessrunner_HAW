using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static bool gameOver;            // Statische Variable, die den Spielzustand speichert (ob das Spiel vorbei ist oder nicht)
    public GameObject gameOverPanel;        // GameObject des Game Over-Panels (Game Over Screen (UI) nach Tod des Spielers)
    public GameObject scoreBackground;      // GameObject des Hintergrunds für die Score-Anzeige (UI)

    public GameObject ScoreText;            // GameObject für den Score-Text (UI)
    public GameObject CoinText;             // GameObject für den Münz-Text (UI)
    
    void Start()                            // Start wird beim ersten Frame aufgerufen
    {
        gameOver = false;                   // Setzt den Spielzustand auf "nicht vorbei" beim Start
    }

    void Update()                           // Update wird einmal pro Frame aufgerufen
    {
        if (gameOver)                       // Überprüft, ob das Spiel vorbei ist
        {
            Time.timeScale = 1;                 // Setzt die Zeit auf normal (um sicherzustellen, dass das Spiel in normaler Geschwindigkeit fortgesetzt wird)
            gameOverPanel.SetActive(true);      // Aktiviert das Game Over-Panel, um es anzuzeigen (UI)
            
            //Deaktiviert die Score-Anzeige (UI) oben links
            scoreBackground.SetActive(false);  
            CoinText.SetActive(false);         
            ScoreText.SetActive(false);         
        }
    }
}
