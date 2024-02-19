using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
   public float skySpeed;   // Geschwindigkeit, mit der sich der Himmel dreht

    void Update()           // Update wird einmal pro Frame aufgerufen
    {
        // Ändert die Rotation des Himmels basierend auf der aktuellen Zeit und der angegebenen Geschwindigkeit
        // Dies erzeugt den Effekt eines sich (langsam) drehenden Himmels
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }
}