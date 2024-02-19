using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatformScript : MonoBehaviour
{
    // Diese Funktion wird aufgerufen wenn eine Plattform in den Trigger-Bereich (GameObject mit Tag "Destroy") eintritt (und wird dann zerstört)

    private void OnTriggerEnter(Collider other)             // Wird aufgerufen, wenn der Collider dieses Objekts mit dem Plattform Collider kollidiert
    {
        if (other.gameObject.CompareTag("Destroy"))         // Überprüft, ob das kollidierende Objekt den Tag "Destroy" hat 
        {
            Destroy(gameObject);                            // Falls ja, Plattform wird zerstört
        }
    }

}
