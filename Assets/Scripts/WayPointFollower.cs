using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;

    [SerializeField] float min_speed = 1f;
    [SerializeField] float max_speed = 2f;

    void Update()
    {
        float randomspeed = Random.Range(min_speed, max_speed);
        
        if(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }

        }
        
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, randomspeed * Time.deltaTime);
    }
}
