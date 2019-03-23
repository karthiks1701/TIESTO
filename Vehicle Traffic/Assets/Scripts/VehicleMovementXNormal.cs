using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleMovementXNormal : MonoBehaviour
{

    private float speed = 10f;
    private Rigidbody rbCurrent;
    private float nextTime;
    private Camera cam;

    private GameObject trafficLineHolder;
    private Color col;

    private bool haventStartedYet = true, haventEndedYet = true;
    private float startTime, endTime, myTime, totalNumberOfCars = 0;
    //private Text meanXText;

    private TrafficLight trafficLight;

    private bool countedOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        rbCurrent = GetComponent<Rigidbody>();
        rbCurrent.velocity = new Vector3(Random.Range(speed - 4f, speed), 0, 0);

        trafficLineHolder = GameObject.FindGameObjectWithTag("TrafficLightHolder");
        trafficLight = trafficLineHolder.GetComponent<TrafficLight>();
        col = trafficLight.materialXPlus.color;

        //meanXText = GameObject.FindGameObjectWithTag("MeanX").GetComponent<Text>();
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
        transform.rotation = Quaternion.Euler(0, 90, 0);
        rbCurrent.velocity = new Vector3(rbCurrent.velocity.x, 0, 0);

        if (6 > transform.position.x && transform.position.x > -6)
        {
            rbCurrent.velocity = new Vector3(13f, 0, 0);
        }
        
        if (col.Equals(Color.red) && haventStartedYet && transform.position.x > -30)
        {
            startTime = Time.time;
            haventStartedYet = false;
        }

        if (!haventStartedYet && haventEndedYet && transform.position.x > -4)
        {
            endTime = Time.time;
            haventEndedYet = false;

            myTime = endTime - startTime;
            trafficLight.getWaitingTime(myTime, 'X');
            /*
            trafficLight.totalTimeX += myTime; ;
            this.totalNumberOfCars = trafficLight.totalNumberOfCarsX;
            Debug.Log(totalNumberOfCars);
            meanXText.text = "Mean Waiting Time in Lane 1 = " + trafficLight.totalTimeX / totalNumberOfCars;
            */
        }

        if (!countedOnce && transform.position.x > 5)
        {
            trafficLight.getCountPassed('X');
            countedOnce = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if ( ( col.Equals(Color.red) || col.Equals(Color.yellow) ) && collision.transform.CompareTag("JeepX") && -13 < collision.transform.position.x && collision.transform.position.x < -5)
        {
            //Debug.Log("Collided in zone with JeepX");
            rbCurrent.velocity = Vector3.zero;
        }
    }

}
