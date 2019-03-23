using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldVersionVehicleMovement : MonoBehaviour
{

    private float speed = 10f;
    public Rigidbody rbCurrent;
    //private bool trigger = false;
    private float nextTime;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rbCurrent = GetComponent<Rigidbody>();
        rbCurrent.velocity = new Vector3(0, 0, Random.Range(speed - 4f, speed));

        cam = Camera.main;
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Jeep"))
        {
            Debug.Log("Jeep");
            rbCurrent.AddForce(0, 0, -0.3f);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(0, 0, 0.3f);
        }
    }
    */
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        rbCurrent.velocity = new Vector3(0, 0, rbCurrent.velocity.z);
        /*
        if (trigger)// && hit.distance > 3f)
        {
            //start increasing
            rbCurrent.velocity += 0.4f * rbCurrent.velocity;
            trigger = false;
        }


        //Ray ray = cam.ScreenPointToRay(;//new Vector3(1.99f, 0.48f, 48f));
        RaycastHit hit;
        //Debug.Log(Input.mousePosition);
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            //Debug.Log(hit.point + " " + hit.transform);
            if (hit.transform.CompareTag("Jeep"))
            {
                if(hit.distance < 1.4f)
                {
                    //stop
                    rbCurrent.velocity = Vector3.zero;
                    trigger = true;
                    hit.transform.GetComponent<Rigidbody>().velocity += 0.4f * hit.transform.GetComponent<Rigidbody>().velocity;
                }
                else if(hit.distance < 2.35f)
                {
                    //start decreasing vel
                    rbCurrent.velocity -= 0.4f * rbCurrent.velocity;
                    trigger = true;
                }
            }
        }
        */

        //if(jeepPrev.position.z - transform.position.z < 2)
        //{
        //decrease velocity
        //}

        //if (trigger && Time.time > nextTime)
        //{
        //other.GetComponent<Rigidbody>().velocity += 0.5f * rbCurrent.velocity;
        //  trigger = false;
        // }

    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jeep"))
        {
            Rigidbody rbOther = other.GetComponent<Rigidbody>();
            if(other.transform.position.z > transform.position.z)
            {
                //rbOther.velocity -= 0.4f * rbOther.velocity; // current noo????
                trigger = true;
                nextTime = Time.time + 0.7f;
            }
            else
            {
                //rbOther.velocity += 0.4f * rbOther.velocity;

            }
            
            
            /*
            if (other.transform.position.z > transform.position.z)
            {
                rbCurrent.velocity -= 0.4f * rbCurrent.velocity * (other.GetComponent<Rigidbody>().velocity.z - transform.position.z) / other.GetComponent<Rigidbody>().velocity.z;
                trigger = true;
                nextTime = Time.time + 0.7f;
            }
            else
            {
                rbCurrent.velocity += 0.4f * rbCurrent.velocity * (-other.GetComponent<Rigidbody>().velocity.z + transform.position.z) / other.GetComponent<Rigidbody>().velocity.z;
            }
            */
    /*
   if (other.transform.position.z > transform.position.z)
   {
       rbCurrent.velocity -= 0.4f * rbCurrent.velocity * (other.transform.position.z - transform.position.z) / other.transform.position.z;
       trigger = true;
       nextTime = Time.time + 0.7f;
   }
   else
   {
       rbCurrent.velocity += 0.4f * rbCurrent.velocity * (-other.transform.position.z + transform.position.z) / other.transform.position.z;
   }
   //


}
}
*/
}
