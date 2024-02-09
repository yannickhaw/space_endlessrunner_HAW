using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    
    public GameObject StartSection;
    public GameObject[] WorldSections;
    public float StartPlayerspeed;
    public float maxSpeed;
    public float acceleration1 = 0.035f;
    public float acceleration2 = 0.02f;
    private float Index = 0;
    public static bool CollisionIndex;

    private void Start()
    {
        
        GameObject StartPlane1 = Instantiate(StartSection, transform);      // Instanziierung und Positionierung von 1 StartTile (so lang wie 3 noemale Tiles) und 4 random Tiles
        StartPlane1.transform.position = new Vector3(0, 0, -5);
    
        int RandomInt1 = Random.Range(0, WorldSections.Length);                         // Generiere zufällig ein Tile für den zweiten Abschnitt

        GameObject TempSection1 = Instantiate(WorldSections[RandomInt1], transform);
        TempSection1.transform.position = new Vector3(0, 0, 25);
        
        
        int RandomInt2 = Random.Range(0, WorldSections.Length);                        // Generiere zufällig ein Tile für den dritten Abschnitt

        GameObject TempSection2 = Instantiate(WorldSections[RandomInt2], transform);
        TempSection2.transform.position = new Vector3(0, 0, 35);

        int RandomInt3 = Random.Range(0, WorldSections.Length);                        // Generiere zufällig ein Tile für den vierten Abschnitt

        GameObject TempSection3 = Instantiate(WorldSections[RandomInt3], transform);
        TempSection3.transform.position = new Vector3(0, 0, 45);
        
        int RandomInt4 = Random.Range(0, WorldSections.Length);                        // Generiere zufällig ein Tile für den fuenften Abschnitt

        GameObject TempSection4 = Instantiate(WorldSections[RandomInt4], transform);
        TempSection4.transform.position = new Vector3(0, 0, 55);
    }

    
    private void Update()
    {
            // Bewege das GameObject nach hinten

        if(GameOverManager.gameOver == false)
        {

            gameObject.transform.position += new Vector3(0, 0, -StartPlayerspeed * Time.deltaTime);

            if(transform.position.z <= Index)                                                   // Wenn die Position auf der z-Achse den Index erreicht oder überschreitet
            {
                int RandomInt1 = Random.Range(0, WorldSections.Length);                         // Generiere zufällig ein Tile 

                GameObject TempSection1 = Instantiate(WorldSections[RandomInt1], transform);
                TempSection1.transform.position = new Vector3(0, 0, 65);

                int RandomInt2 = Random.Range(0, WorldSections.Length);                         // Generiere zufällig ein Tile

                GameObject TempSection2 = Instantiate(WorldSections[RandomInt2], transform);
                TempSection2.transform.position = new Vector3(0, 0, 75);

           
                Index = Index - 20f;            // Aktualisiere den Index für die nächste Überprüfung
            }

        
        if (StartPlayerspeed < maxSpeed)
        {
            StartPlayerspeed += acceleration1 * Time.deltaTime;    //Bestimmt die Geschwindigkeitserhöhung in der die Tiles auf den Player zukommen
        }
        else
        {
            StartPlayerspeed += acceleration2 * Time.deltaTime;
        }
                
        //Debug.Log("PlayerSpeed:  " + StartPlayerspeed);
           
        }
        
    }
}
