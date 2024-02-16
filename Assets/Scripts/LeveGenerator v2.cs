using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    
    public GameObject ScoreSection;                                 // GameObject fuer ScoreSection
    public GameObject StartSection;                                 // GameObject fuer StartSection
    public GameObject[] WorldSections;                              // Array fuer die Random Level Sections
    public float StartPlayerspeed;                                  // Startgeschwindigkeit des Spielers
    public float maxSpeed;                                          // Bis zu dieser Geschwindigkeit des Spielers wird mit Beschleunigungswert 1 beschleunigt (danach mit Beschleunigungswert 2)
    public float acceleration1 = 0.035f;                            // Beschleunigungswert 1
    public float acceleration2 = 0.02f;                             // Beschleunigungswert 2
    private float Index = 0;                                        // Index zur Überprüfung, wann neue Abschnitte generiert werden müssen
    
    private void Start()
    {
        // Erstellung des Startabschnitts
        GameObject StartPlane1 = Instantiate(StartSection, transform);      // Startabschnitterstellung (so lang wie 3 normale Abschnitte)
        StartPlane1.transform.position = new Vector3(0, 0, -5);

        // Erstellung von vier zufälligen Abschnitten nach dem Startabschnitt
        for (int i = 0; i < 4; i++)
        {
            int RandomInt = Random.Range(0, WorldSections.Length);
            GameObject TempSection = Instantiate(WorldSections[RandomInt], transform);
            TempSection.transform.position = new Vector3(0, 0, 25 + i * 10);                // Positionierung der Sections in bestimmten Abständen (Abstand entspricht Laenge einer Section)
        }
    }

    
    private void Update()
    {
    
        if(GameOverManager.gameOver == false)                                                   // Überpruefe, ob das Spiel vorbei ist
        {

            gameObject.transform.position += new Vector3(0, 0, -StartPlayerspeed * Time.deltaTime);     // Bewegung des Level-Generators in Richtung des Spielers

            if(transform.position.z <= Index)                                                   // Überprüfe, ob der Indexwert erreicht wurde, um neue Abschnitte zu generieren
            {
                // Generierung von zwei zufälligen Sections
                for (int i = 0; i < 2; i++)
                {
                    int RandomInt = Random.Range(0, WorldSections.Length);
                    GameObject TempSection = Instantiate(WorldSections[RandomInt], transform);
                    TempSection.transform.position = new Vector3(0, 0, 65 + i * 10);            // Positionierung der Sections in bestimmten Abständen
                }

                // Erstellung von zwei Sections für den Score Counter (zählt Score hoch)
                for (int i = 0; i < 2; i++)
                {
                    GameObject TempScoreSection = Instantiate(ScoreSection, transform);
                    TempScoreSection.transform.position = new Vector3(0, 0, 2 + i * 10);        // Positionierung der Score-Section in bestimmten Abständen
                }
           
                Index = Index - 20f;            // Aktualisiere den Index für die nächste Überprüfung
            }


            if (StartPlayerspeed < maxSpeed)                           // Anpassung der Spielergeschwindigkeit basierend auf der Beschleunigung
            {
                StartPlayerspeed += acceleration1 * Time.deltaTime;    //Beschleunigungswert 1 --> Bestimmt die Geschwindigkeitserhöhung in der die Sections auf den Player zukommen (bzw. einfach Beschleunigung des Levels)
            }
            else
            {
                StartPlayerspeed += acceleration2 * Time.deltaTime;    //Beschleunigungswert 2 --> Beim erreichen einer bestimmten Geschwindigkeit, wird das Level nicht mehr ganz so schnell beschleunigt
            }

        }
    }
}
