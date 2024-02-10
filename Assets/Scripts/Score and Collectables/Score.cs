using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
  //  public Text scoreText;                          // Hier fügst du das Text-Objekt hinzu, das deinen Punktestand anzeigt
    public TextMeshProUGUI scoreUI;

    public TextMeshProUGUI highscoreUI;
    public TextMeshProUGUI highscoreUI2;

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

        //highscoreUI.text = "New Highscore!!! (" + Mathf.Round(Highscore).ToString() + "m)";
        highscoreUI2.text = "Best: " + Mathf.Round(Highscore).ToString() + "m";
        
        if(GameOverManager.gameOver == false)
        {
            //currentScore += scorePerSecond * Time.deltaTime;
            currentScore = Score_Trigger.score_count;
        }

        else if (GameOverManager.gameOver == true && currentScore > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", currentScore);
            highscoreUI.text = "New Highscore!!! (" + Mathf.Round(currentScore).ToString() + "m)";
            Highscore = PlayerPrefs.GetFloat("Highscore");
        }



        //scoreUI.text += scorePerSecond * Time.deltaTime;
        // Aktualisiere den angezeigten Punktestand im Text-Objekt
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreUI.text = " " + Mathf.Round(currentScore).ToString() + "m";

    }
}