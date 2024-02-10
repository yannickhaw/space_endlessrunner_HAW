using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject scoreBackground;

    public GameObject ScoreText;
    public GameObject CoinText;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 1;
            gameOverPanel.SetActive(true);
            //scoreBackground.SetActive(false);
            //CoinText.SetActive(false);
            //ScoreText.SetActive(false);
        }
    }
}
