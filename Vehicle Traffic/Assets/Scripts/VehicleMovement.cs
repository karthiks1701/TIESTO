using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleMovement : MonoBehaviour
{

    private float speed = 10f;
    private Rigidbody rbCurrent;
    //private bool trigger = false;
    private float nextTime;
    private Camera cam;

    private GameObject trafficLineHolder;
    private Color col;

    private bool haventStartedYet = true, haventEndedYet = true;
    private float startTime, endTime, myTime, totalNumberOfCars = 0;
    //private Text meanZText;

    private TrafficLightAI trafficLightAI;

    private bool countedOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        rbCurrent = GetComponent<Rigidbody>();
        rbCurrent.velocity = new Vector3(0, 0, Random.Range(speed - 4f, speed));

        trafficLineHolder = GameObject.FindGameObjectWithTag("TrafficLightHolder");
        trafficLightAI = trafficLineHolder.GetComponent<TrafficLightAI>();
        col = trafficLightAI.materialZPlus.color;

        //meanZText = GameObject.FindGameObjectWithTag("MeanZ").GetComponent<Text>();
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
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rbCurrent.velocity = new Vector3(0, 0, rbCurrent.velocity.z);
        
        if(6 > transform.position.z && transform.position.z > -6)
        {
            rbCurrent.velocity = new Vector3(0, 0, 13f);
        }
        
        if (col.Equals(Color.red) && haventStartedYet && transform.position.z > -30)
        {
            startTime = Time.time;
            haventStartedYet = false;
        }
        if (!haventStartedYet && haventEndedYet && transform.position.z > -4)
        {
            endTime = Time.time;
            haventEndedYet = false;
            
            myTime = endTime - startTime;
            trafficLightAI.getWaitingTime(myTime, 'Z');
            /*
            trafficLightAI.totalTimeZ += myTime;
            trafficLightAI.totalNumberOfCarsZ++;
            Debug.Log(totalNumberOfCars);
            meanZText.text = "Mean Waiting Time in Lane 2 = " + trafficLightAI.totalTimeZ / totalNumberOfCars;
            */    
        }

        if (!countedOnce && transform.position.z > 5)
        {
            trafficLightAI.getCountPassed('Z');
            countedOnce = true;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if ( ( col.Equals(Color.red) || col.Equals(Color.yellow) ) && collision.transform.CompareTag("Jeep") && -13 < collision.transform.position.z && collision.transform.position.z < -5)
        {
            //Debug.Log("Collided in zone with Jeep");
            rbCurrent.velocity = Vector3.zero;
        }
    }

}
