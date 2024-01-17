using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    //public AudioSource coinFx;    für coin sound effekt später

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }



}
