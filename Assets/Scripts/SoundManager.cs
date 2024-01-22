using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.loop = s.loop;
            s.audioSource.volume = s.volume;
        }
        
        PlaySound("MainThemeMusic");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.gameOver == true)
        {
           StopSound("MainThemeMusic");
        }


    }


    public void PlaySound(string name)
    {
        foreach(Sound s in sounds)
        {
            if (s.name == name)
            
                s.audioSource.Play();
            
        }
    }

    public void StopSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)

                s.audioSource.Stop();
        }
    }



}
