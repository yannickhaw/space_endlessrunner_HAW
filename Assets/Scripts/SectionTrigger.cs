using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
   public GameObject marsRoadSection;

   private void OnTriggerEnter(Collider other)
   {

        if (other.gameObject.CompareTag("GeneratorTrigger"))
        {
            Instantiate(marsRoadSection, new Vector3(0, 0, 25.4f), Quaternion.identity);
        }
   }
}
