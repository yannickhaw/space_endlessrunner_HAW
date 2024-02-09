using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject playerObject;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyObstacle"))
        {
            playerObject.GetComponent<Animator>().Play("Suprise");
            FindObjectOfType<SoundManager>().PlaySound("DeathSound");

            GameOverManager.gameOver = true;
        }
    }
}
