using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_custom_Object : MonoBehaviour
{
    public float rotateSpeed;
  
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);    
    }
}
