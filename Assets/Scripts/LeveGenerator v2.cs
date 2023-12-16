using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    
    public GameObject StartSection;

    public GameObject[] WorldSections;

    public float StartPlayerspeed = 5f;

    private float Index = 0;
    
    public float CollisionIndex = 0;
        
    public void OnPlayerDeath()
    {
        //GetComponent<PlayerCollision>().SetPlayerDied();
        Debug.Log("cololololllission");
        CollisionIndex = 1;
    }

    private void Start()
    {
        
     /* GameObject StartPlane1 = Instantiate(StartSection, transform);
        StartPlane1.transform.position = new Vector3(0, 0, 31);
        GameObject StartPlane2 = Instantiate(StartSection, transform);
        StartPlane2.transform.position = new Vector3(0, 0, 21);
     */  GameObject StartPlane1 = Instantiate(StartSection, transform);      // Instanziierung und Positionierung von 3 StartTiles
        StartPlane1.transform.position = new Vector3(0, 0, -5);
        GameObject StartPlane2 = Instantiate(StartSection, transform);
        StartPlane2.transform.position = new Vector3(0, 0, 5);
        GameObject StartPlane3 = Instantiate(StartSection, transform);
        StartPlane3.transform.position = new Vector3(0, 0, 15);
    
        int RandomInt1 = Random.Range(0, WorldSections.Length);                         // Generiere zufällig ein Tile1 oder Tile2 für den zweiten Abschnitt

        GameObject TempSection1 = Instantiate(WorldSections[RandomInt1], transform);
        TempSection1.transform.position = new Vector3(0, 0, 25);
        
        
        int RandomInt2 = Random.Range(0, WorldSections.Length);                        // Generiere zufällig ein Tile1 oder Tile2 für den zweiten Abschnitt
        GameObject TempSection2 = Instantiate(WorldSections[RandomInt2], transform);
        TempSection2.transform.position = new Vector3(0, 0, 35);
        
    
        
    }

    
    private void Update()
    {
            // Bewege das GameObject nach hinten

        if(CollisionIndex == 0)
        {

            gameObject.transform.position += new Vector3(0, 0, -StartPlayerspeed * Time.deltaTime);

            if(transform.position.z <= Index)                                // Wenn die Position auf der z-Achse den Index erreicht oder überschreitet
            {
                int RandomInt1 = Random.Range(0, WorldSections.Length);                         // Generiere zufällig ein Tile1 oder Tile2 für den zweiten Abschnitt

                GameObject TempSection1 = Instantiate(WorldSections[RandomInt1], transform);
                TempSection1.transform.position = new Vector3(0, 0, 45);

                int RandomInt2 = Random.Range(0, WorldSections.Length);                        // Generiere zufällig ein Tile1 oder Tile2 für den zweiten Abschnitt

                GameObject TempSection2 = Instantiate(WorldSections[RandomInt2], transform);
                TempSection2.transform.position = new Vector3(0, 0, 55);

           
                Index = Index - 20f;  

            
                /*StartPlayerspeed = 10* Mathf.Log(StartPlayerspeed);            
                Debug.Log(StartPlayerspeed);
                StartPlayerspeed = StartPlayerspeed  * 1.1f; */
                //StartPlayerspeed = StartPlayerspeed  +  0.1f; 
                Debug.Log(StartPlayerspeed);

                                                  // Aktualisiere den Index für die nächste Überprüfung
            }
           
        }
        
    }
}
