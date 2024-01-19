using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinScore : MonoBehaviour
{
  //  public Text scoreText;                          // Hier fügst du das Text-Objekt hinzu, das deinen Punktestand anzeigt
    public TextMeshProUGUI scoreUI;

    private int currentScore = 0;

    public void ScorePlusOne()
    {
        currentScore++;
    }

    void UpdateScoreText()
    {
        // Zeige den Punktestand im Text-Objekt an
        scoreUI.text = "Coins: " +  currentScore.ToString();
    }
}