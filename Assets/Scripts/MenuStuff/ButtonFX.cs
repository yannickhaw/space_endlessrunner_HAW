using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    public AudioSource myFx;   // Referenz auf die AudioSource-Komponente, die die Soundeffekte abspielen soll
    public AudioClip hoverFx;  // Soundeffekt, der beim Hovern über den Button abgespielt werden soll
    public AudioClip clickFx;  // Soundeffekt, der beim Klicken auf den Button abgespielt werden soll

    public void HoverSound()               // wird aufgerufen, wenn der Mauszeiger über den Button bewegt wird
    {
        myFx.PlayOneShot(hoverFx);         // Spielt den Hover-Soundeffekt einmal ab
    }

    public void ClickSound()               // wird aufgerufen, wenn der Button geklickt wird
    {
        myFx.PlayOneShot(clickFx);         // Spielt den Click-Soundeffekt einmal ab
    }
}
