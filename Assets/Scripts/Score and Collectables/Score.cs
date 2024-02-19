using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;             // Hier wird das Text-Objekt hinzugefügt, das den Score anzeigt
    public TextMeshProUGUI highscoreUI;         // Hier wird das Text-Objekt hinzugefügt, das den High-Score im GameOver Screen anzeigt
    public TextMeshProUGUI highscoreUI2;        // Hier wird das Text-Objekt hinzugefügt, das den High-Score während des Spiels anzeigt
    private float currentScore = 0f;            // Aktueller Spiel-Score
    public float Highscore;                     // Aktueller Highscore

    void Start()        // Start wird beim ersten Frame aufgerufen
    {
        if(PlayerPrefs.HasKey("Highscore"))                 // Überprüft, ob bereits ein Highscore-Wert in den Spieler-Präferenzen gespeichert ist
        {
            Highscore = PlayerPrefs.GetFloat("Highscore");  // Highscore wird aus den Spielerpräferenzen abgerufen
        }
        else
        {
            Highscore = 0;                                  // Wenn keine Daten vorhanden sind, wird Highscore auf 0 gesetzt
        }
    }


    void Update()        // Update wird einmal pro Frame aufgerufen 
    {
        highscoreUI2.text = "Best: " + Mathf.Round(Highscore).ToString() + "m";     // zeigt Highscore während des Spiels an
        
        if(GameOverManager.gameOver == false)                                       // Überprüft, ob das Spiel noch nicht vorbei ist
        {
            currentScore = Score_Trigger.score_count;                               // Aktualisierung des Scores aus einer anderen Quelle
        }

        else if (GameOverManager.gameOver == true && currentScore > PlayerPrefs.GetFloat("Highscore"))  // Überprüft, ob das Spiel vorbei ist und der score größer als der gespeicherte highscore ist
        {
            PlayerPrefs.SetFloat("Highscore", currentScore);                                            // Der neue Highscore wird in den Spielerpräferenzen gespeichert
            highscoreUI.text = "New Highscore!!! (" + Mathf.Round(currentScore).ToString() + "m)";      // Highscore wird im Game Over screen angezeigt (UI)
            Highscore = PlayerPrefs.GetFloat("Highscore");                                              // Highscore wird aktualisiert
        }

        UpdateScoreText();                                                          // Aktualisiert den angezeigten Score im Text-Objekt
    }

    void UpdateScoreText()
    {
        scoreUI.text = " " + Mathf.Round(currentScore).ToString() + "m";            // Zeigt aktuellen Score im Text-Objekt an

    }
}