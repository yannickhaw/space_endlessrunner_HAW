using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinscoreUI;
    public TextMeshProUGUI totalcoinscoreUI;
    int coincount = 0;
    int totalcoins;

    void Start()
    {
        if(PlayerPrefs.HasKey("TotalCoins"))
        {
            totalcoins = PlayerPrefs.GetInt("TotalCoins");
        }
        else
        {
            totalcoins = 0;
        }
        totalcoinscoreUI.text = "" + totalcoins;
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            FindObjectOfType<SoundManager>().PlaySound("CoinSFX");
            Destroy(other.gameObject);
            coincount++;
            coinscoreUI.text = "" + coincount;

            totalcoins++;        
            PlayerPrefs.SetInt("TotalCoins", totalcoins);
            totalcoinscoreUI.text = "" + totalcoins;
        }
    }
}
