using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCoinsandScore : MonoBehaviour
{
    
    public TextMeshProUGUI highscoretext;
    public TextMeshProUGUI totalcoinstext;

    private float Highscore;
    private int totalcoins;
    
    // Start is called before the first frame update
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

        if(PlayerPrefs.HasKey("TotalCoins"))
        {
            totalcoins = PlayerPrefs.GetInt("TotalCoins");
        }
        else
        {
            totalcoins = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        highscoretext.text = "Best: " + Mathf.Round(Highscore).ToString() + "m";
        totalcoinstext.text = "      " + totalcoins;
    }


}