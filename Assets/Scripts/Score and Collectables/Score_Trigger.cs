using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Score_Trigger : MonoBehaviour
{
    public static int score_count = 0;

    void Start()
    {
        score_count = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ScoreTrigger"))
        {
            score_count++;
        }
    }
}
