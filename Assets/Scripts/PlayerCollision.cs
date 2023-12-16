using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject playerObject;
    private LevelGenerator levelGenerator;

    private void Start()
    {
        // Assuming PlayerDeath script is on the same GameObject as LevelGenerator script
        //levelGenerator = GetComponent<LevelGenerator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyObstacle"))
        {
            
            playerObject.GetComponent<Animator>().Play("Stumble Backwards");
            //Destroy(gameObject);
            
            Debug.Log("You Die");
                        
            StartCoroutine(ReloadLevelWithDelay(1.5f));         // Start the ReloadLevel coroutine with a delay

            //ReloadLevel();          //Ruft die ReloadLevel Funktion auf und führt sie nach 1,5 Sekunden aus (Delay um Neustart zu verlängern)
            


        }
    }

    IEnumerator ReloadLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //Lädt das Level von vorne
    }

}
