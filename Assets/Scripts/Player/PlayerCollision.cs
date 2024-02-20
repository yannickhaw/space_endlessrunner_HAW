using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject playerObject;                                             // Referenz für Animation des Spielers

    public void OnTriggerEnter(Collider other)                                  // Wird aufgerufen, wenn der Collider dieses Objekts mit dem Spieler Collider (Hit-Box) kollidiert
    {
        if(other.gameObject.CompareTag("EnemyObstacle"))                        // Überprüft, ob das kollidierende Objekt den Tag "EnemyObstacle" hat (also ein Hindernis ist)
        {
            playerObject.GetComponent<Animator>().Play("Suprise");              // Spielt eine Animation des Spielers ab
            FindObjectOfType<SoundManager>().PlaySound("DeathSound");           // Spielt SFX ab

            GameOverManager.gameOver = true;                                    // Setzt die gameOver-Variable im GameOverManager auf true, um das Spiel zu beenden
        }
    }
}
