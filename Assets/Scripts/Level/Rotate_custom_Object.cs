using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_custom_Object : MonoBehaviour
{
    public float rotateSpeed;               // Geschwindigkeit mit der das Objekt rotieren soll
  
    void Update()           // Update wird einmal pro Frame aufgerufen
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);    // Rotiert das GameObject um die y-Achse im Weltkoordinatensystem 
    }
}
