using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    private GameOverManager player;
    
    TextMeshProUGUI HighscoreText;
    public float highScore;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<GameOverManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HighscoreText.text = highScore.ToString();



    }


    public void Die()
    {
        
    }
}
