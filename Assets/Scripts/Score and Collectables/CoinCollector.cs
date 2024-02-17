using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinscoreUI;                 // UI-Text für den aktuelle Münz-Score
    public TextMeshProUGUI totalcoinscoreUI;            // UI-Text für die insgesamt gesammelten Münzen  
    int coincount = 0;                                  // Aktueller Münz-Score
    int totalcoins;                                     // insgesamt gesammelte Münzen

    void Start()
    {
        if(PlayerPrefs.HasKey("TotalCoins"))                // Überprüft, ob die Anzahl der insgesamt gesammelten Münzen bereits in den Spieler Präferenzen gespeichert ist
        {
            totalcoins = PlayerPrefs.GetInt("TotalCoins");  // Gesamtanzahl wird aus den Spielerpräferenzen abgerufen
        }
        else
        {
            totalcoins = 0;                                 // Wenn keine Daten vorhanden sind, wird Gesamtanzahl auf 0 gesetzt
        }
        totalcoinscoreUI.text = "" + totalcoins;            // Gesamtanzahl der Münzen wird in der UI aktualisiert
    }
    
    
    private void OnTriggerEnter(Collider other)             // Wird aufgerufen, wenn der Collider dieses Objekts mit dem Spieler Collider (Hit-Box) kollidiert
    {
        if (other.gameObject.CompareTag("Coin"))            // Überprüft, ob das kollidierende Objekt den Tag "Coin" hat (also eine Münze ist)
        {
            FindObjectOfType<SoundManager>().PlaySound("CoinSFX");      // Münz-SFX wird abgespielt
            Destroy(other.gameObject);                                  // Münze wird zerstört
            coincount++;                                                // Münz-Score wird um eins erhöht
            coinscoreUI.text = "" + coincount;                          // AKtueller Münz-Score wird in der UI aktualisiert

            totalcoins++;                                               // Gesamtanzahl der Münzen wird ebenfalls um eins erhöht
            PlayerPrefs.SetInt("TotalCoins", totalcoins);               // Gesamtanzahl der Münzen wird in den Spielerpräferenzen gespeichert
            totalcoinscoreUI.text = "" + totalcoins;                    // Gesamtanzahl der Münzen wird in der UI aktualisiert
        }
    }
}
