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
    
    void Start()                                   // Start wird beim ersten Frame aufgerufen
    {
        if(PlayerPrefs.HasKey("Highscore"))                     // Überprüft, ob bereits ein Highscore-Wert in den Spieler-Präferenzen gespeichert ist
        {
            Highscore = PlayerPrefs.GetFloat("Highscore");      // Highscore wird aus den Spielerpräferenzen abgerufen
        }
        else
        {
            Highscore = 0;                                      // Wenn keine Daten vorhanden sind, wird Highscore auf 0 gesetzt
        }

        if(PlayerPrefs.HasKey("TotalCoins"))                    // Überprüft, ob die Anzahl der insgesamt gesammelten Münzen bereits in den Spieler Präferenzen gespeichert ist
        {
            totalcoins = PlayerPrefs.GetInt("TotalCoins");      // Gesamtanzahl der gesammelten Münzen wird aus den Spielerpräferenzen abgerufen
        }
        else
        {
            totalcoins = 0;                                     // Wenn keine Daten vorhanden sind, wird Gesamtanzahl auf 0 gesetzt
        }
    }

    void Update()           // Update wird einmal pro Frame aufgerufen 
    {
        highscoretext.text = "Best: " + Mathf.Round(Highscore).ToString() + "m";        // Zeigt aktuellen Highscore im Text-Objekt an
        totalcoinstext.text = "      " + totalcoins;                                    // Zeigt aktuellen Münz-Score im Text-Objekt an
    }


}