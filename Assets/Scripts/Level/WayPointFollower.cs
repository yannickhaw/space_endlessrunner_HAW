using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    // Skript wird an einem GameObject befestigt, das sich entlang einer Reihe von Wegpunkten bewegen soll. Die Wegpunkte werden im Inspector als Array von GameObjects zugewiesen
    
    [SerializeField] GameObject[] waypoints;  // Array von Wegpunkten, denen das Objekt folgen soll
    int currentWaypointIndex = 0;             // Index des aktuellen Wegpunkts, dem das Objekt folgt
    public float min_speed = 1f;              // minimale Geschwindigkeit, mit der sich das Objekt bewegt
    public float max_speed = 2f;              // maximale Geschwindigkeit, mit der sich das Objekt bewegt

    void Update()           // Update wird einmal pro Frame aufgerufen
    {
        float randomspeed = Random.Range(min_speed, max_speed);         // Berechnung zufälliger Geschwindigkeit zwischen min_speed und max_speed
        
        if(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)      // Überprüft, ob das Objekt nahe genug am aktuellen Wegpunkt ist
        {
            currentWaypointIndex++;                                                                             // Wenn ja, wird Index des aktuellen Wegpunkts um 1 erhöht

            if (currentWaypointIndex >= waypoints.Length)                                                       // Überprüft, ob der Index des aktuellen Wegpunkts das Ende des Arrays erreicht hat
            {
                currentWaypointIndex = 0;                                                                       // Wenn ja, Index = 0, also weg wird von vorne gestartet
            }
        }

        // Bewegt das Objekt schrittweise zum nächsten Wegpunkt mit der zufälligen Geschwindigkeit
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, randomspeed * Time.deltaTime);
    }
}
