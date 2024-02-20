using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]                   // Macht die Klasse serialisierbar, sodass Instanzen im Unity Inspector angezeigt und bearbeitet werden können
public class Sound
{
    public string name;                 // Name des Sounds
    public AudioClip audioClip;         // Audiodatei des Sounds

    public float volume;                // Lautstärke des Sounds
    public bool loop;                   // Ein boolscher Wert, der angibt, ob der Sound in einer Endlosschleife abgespielt werden soll
    public AudioSource audioSource;     // Audio-Quelle, die für die Wiedergabe des Sounds verwendet wird

}
