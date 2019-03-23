using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jeep") || other.CompareTag("JeepX"))
        {
            Destroy(other.gameObject);  
            //other.gameObject.SetActive(false);
        }
    }
}
