using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficStopSrcipt : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Jeep"))
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
