using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
  //  public Text scoreText;                          // Hier fügst du das Text-Objekt hinzu, das deinen Punktestand anzeigt
    public TextMeshProUGUI scoreUI;

    public TextMeshProUGUI highscoreUI;

    public float scorePerSecond = 10f;                // Punktestand pro Sekunde
    private float currentScore = 0f;

    public float Highscore;

    void Start()
    {
        if(PlayerPrefs.HasKey("Highscore"))
        {
            Highscore = PlayerPrefs.GetFloat("Highscore");
        }
        else
        {
            Highscore = 0;
        }
    }

    void Update()
    {
        // Erhöhe den Punktestand basierend auf der Zeit und der vorgegebenen Punkte pro Sekunde
        //scoreUI.text = scoreText.ToString();

        highscoreUI.text = Highscore.ToString();
        
        if(GameOverManager.gameOver == false)
        {
            currentScore += scorePerSecond * Time.deltaTime;

        }

        else if (GameOverManager.gameOver == true && currentScore > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", currentScore);
            Highscore = PlayerPrefs.GetFloat("Highscore");
        }



        //scoreUI.text += scorePerSecond * Time.deltaTime;
        // Aktualisiere den angezeigten Punktestand im Text-Objekt
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreUI.text = "Score: " + Mathf.Round(currentScore).ToString();

    }
}