using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody ribo;
    public float backMovement = -5;

    public bool isDead = false;
    public GameObject playerObject;
    private LevelGenerator levelGenerator;

    private void Start()
    {
        // Assuming PlayerDeath script is on the same GameObject as LevelGenerator script
        //levelGenerator = GetComponent<LevelGenerator>();
        ribo = GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyObstacle"))
        {
            
            playerObject.GetComponent<Animator>().Play("Stumble Backwards");
            FindObjectOfType<SoundManager>().PlaySound("DeathSound");
            //Destroy(gameObject);
            
            //ribo.velocity = new Vector3(0, 0, -5);
            PlayerManager.gameOver = true;
            isDead = true;
            //Debug.Log("You Die");
                        
            StartCoroutine(ReloadLevelWithDelay(2.0f));         // Start the ReloadLevel coroutine with a delay

            //ReloadLevel();          //Ruft die ReloadLevel Funktion auf und führt sie nach 1,5 Sekunden aus (Delay um Neustart zu verlängern)
            


        }
    }

    void Update()
    {
        if (isDead)
        {
            //transform.Translate(Vector3.forward * backMovement * Time.deltaTime);
            transform.Translate(Vector3.up * 0 * Time.deltaTime);           //funktioniert nicht aber so ähnlich
            transform.Translate(Vector3.right * 0 * Time.deltaTime);
           //ribo.velocity = new Vector3(0, 0, backMovement);
            //make a function to stick to platform
        }
        

    }
    IEnumerator ReloadLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //Lädt das Level von vorne
    }

}
