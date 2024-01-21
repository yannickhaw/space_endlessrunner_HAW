using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinscoreUI;
    int coincount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            FindObjectOfType<SoundManager>().PlaySound("CoinSFX");
            Destroy(other.gameObject);
            coincount++;
            coinscoreUI.text = "Coins: " + coincount;

        }
    }
}
