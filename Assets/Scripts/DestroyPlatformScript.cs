using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformScript : MonoBehaviour
{
    // Diese Funktion wird aufgerufen wenn eine Plattform in den Trigger bereich (GameObject mit Tag "Destroy") eintritt (und wird dann zerstört)

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))        
        {
            Destroy(gameObject);                    
        }
    }

}
