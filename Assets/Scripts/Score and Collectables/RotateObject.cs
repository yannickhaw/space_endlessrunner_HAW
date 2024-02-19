using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private float rotateSpeed = 200;            // Geschwindigkeit mit der das Objekt rotieren soll
  
    void Update()                               // Update wird einmal pro Frame aufgerufen 
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);         // Rotiert das GameObject um die y-Achse im Weltkoordinatensystem 
    }
}
