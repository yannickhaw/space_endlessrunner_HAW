using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Score_Trigger : MonoBehaviour
{
    public static int score_count = 0;              // Variable zur Speicherung des aktuellen Scores (zählt Score hoch)

    void Start()                                    // Start wird beim ersten Frame aufgerufen       
    {
        score_count = 0;                            // Setzt den Score zu Beginn des Spiels auf 0
    }
    private void OnTriggerEnter(Collider other)               // Wird aufgerufen, wenn der Collider dieses Objekts mit dem Spieler Collider (Hit-Box) kollidiert
    {
        if (other.gameObject.CompareTag("ScoreTrigger"))      // Überprüft, ob das kollidierende Objekt den Tag "ScoreTrigger" hat     
        {
            score_count++;                                    // Score wird um eins erhöht
        }
    }
}
