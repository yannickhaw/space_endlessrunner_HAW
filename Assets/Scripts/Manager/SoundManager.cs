using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;                                              // Ein Array von Soundobjekten, die die verschiedenen Sounds im Spiel repräsentieren
    
    void Start()                                                        // Start wird einmalig beim ersten Frameaufruf aufgerufen
    {
        foreach(Sound s in sounds)                                      // Durchläuft alle Soundobjekte im Array
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();     // Fügt jedem Soundobjekt eine AudioSource-Komponente hinzu

            s.audioSource.clip = s.audioClip;                           // Setzt die entsprechenden Audio-Datei zur Audio Quelle
            s.audioSource.loop = s.loop;                                // Setzt die Endlosschleifen-Option auf das Soundobjekt
            s.audioSource.volume = s.volume;                            // Setzt die Lautstärke-Option auf das Soundobjekts
        }
        
        // Ausgeführte Sounds beim Start des Spiels
        PlaySound("StartExplosionSFX");                                 // Explosions SFX
        PlaySound("MainThemeMusic");                                    // Hintergrundmusik
    }

    // Update is called once per frame
    void Update()                                                       // Update wird einmal pro Frame aufgerufen
    {
        if (GameOverManager.gameOver == true)                           // Überprüft, ob das Spiel vorbei ist
        {
            StopSound("MainThemeMusic");                                // Hintergrundmusik wird gestoppt
        }
    }


    public void PlaySound(string name)                                  // Funktion zum Abspielen eines Sounds anhand seines Namens
    {
        foreach(Sound s in sounds)                                      // Durchläuft alle Soundobjekte im Array
        {
            if (s.name == name)                                         // Überprüft, ob der Name des aktuellen Soundobjekts mit dem angegebenen Namen übereinstimmt
            {
                s.audioSource.Play();                                   // Wenn ja, wird der Sound über die zugehörige AudioSource-Komponente abgespielt
            }       
        }
    }

    public void StopSound(string name)                                  // Funktion zum Stoppen eines Sounds anhand seines Namens
    {
        foreach (Sound s in sounds)                                     // Durchläuft alle Soundobjekte im Array
        {
            if (s.name == name)                                         // Überprüft, ob der Name des aktuellen Soundobjekts mit dem angegebenen Namen übereinstimmt 
            {
                s.audioSource.Stop();                                   // Wenn ja, wird der Sound über die zugehörige AudioSource-Komponente gestoppt
            }
        }
    }
}
