using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Endscore : MonoBehaviour
{
  //  public Text scoreText;                          // Hier fügst du das Text-Objekt hinzu, das deinen Punktestand anzeigt
    public TextMeshProUGUI scoreUI;

    public float scorePerSecond = 10f;                // Punktestand pro Sekunde
    private float currentEndScore = 0f;

    void Update()
    {
        // Erhöhe den Punktestand basierend auf der Zeit und der vorgegebenen Punkte pro Sekunde
        //scoreUI.text = scoreText.ToString();
        if (PlayerManager.gameOver == false)
        {
            currentEndScore += scorePerSecond * Time.deltaTime;

        }
        
        //scoreUI.text += scorePerSecond * Time.deltaTime;
        // Aktualisiere den angezeigten Punktestand im Text-Objekt
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // Zeige den Punktestand im Text-Objekt an
        
        scoreUI.text = "Score: " + Mathf.Round(currentEndScore).ToString();

    }
}