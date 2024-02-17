using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Endscore : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;                    // Hier wird das Text-Objekt hinzugefügt, das den Score anzeigt
    private float currentEndScore = 0f;                // Aktueller End-Score

    void Update()                                      // Update wird einmal pro Frame aufgerufen
    {
        if (GameOverManager.gameOver == false)          // Überprüft, ob das Spiel noch nicht vorbei ist
        {
            currentEndScore = Score_Trigger.score_count;    // Aktualisierung des End-Scores aus einer anderen Quelle
        }

        UpdateScoreText();                              // Aktualisierung des Scores
    }

    void UpdateScoreText()                              // Funktion zum Aktualisieren des angezeigten Score im Text-Objekt
    {
        scoreUI.text = "" + Mathf.Round(currentEndScore).ToString() + "m";      // Zeigt den Score im Text-Objekt an
    }
}