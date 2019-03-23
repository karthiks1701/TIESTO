using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightStatic : MonoBehaviour
{
    public Material materialZPlus, materialZMinus, materialXPlus, materialXMinus;
    private float nextTime = 0;//, prevTime;
    private int i = 0;

    public GameObject trafficLightZPlus, trafficLightZMinus, trafficLightXPlus, trafficLightXMinus;

    public GameObject trafficBlockerPlaneZ;
    public GameObject trafficBlockerPlaneX;

    private Rigidbody rbCurrent;
    private GameObject[] jeeps;
    //private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        trafficBlockerPlaneZ.SetActive(false);
        trafficBlockerPlaneX.SetActive(true);
        materialZPlus = trafficLightZPlus.GetComponent<Renderer>().material;
        materialZMinus = trafficLightZMinus.GetComponent<Renderer>().material;
        materialXPlus = trafficLightXPlus.GetComponent<Renderer>().material;
        materialXMinus = trafficLightXMinus.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            if (i % 4 == 0)
            {
                //jeeps = GameObject.FindGameObjectsWithTag("JeepX");

                materialZPlus.color = materialZMinus.color = Color.red;
                trafficBlockerPlaneZ.SetActive(true);
                trafficBlockerPlaneX.SetActive(false);
                /*
                for (int i = 0; i < jeeps.Length; i++)
                {
                    if (jeeps[i].transform.position.x < -4.7f)
                    {
                        jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(speed - 4f, speed), 0, 0);
                    }
                }
                */
                materialXPlus.color = materialXMinus.color = Color.green;
            }
            else if (i % 4 == 1)
            {
                materialZPlus.color = materialZMinus.color = Color.yellow;
                materialXPlus.color = materialXMinus.color = Color.yellow;

                trafficBlockerPlaneZ.SetActive(true);
                trafficBlockerPlaneX.SetActive(true);
            }
            else if (i % 4 == 2)
            {
               // jeeps = GameObject.FindGameObjectsWithTag("Jeep");

                materialZPlus.color = materialZMinus.color = Color.green;
                //delay????????
                trafficBlockerPlaneX.SetActive(true);
                trafficBlockerPlaneZ.SetActive(false);
                /*
                for (int i = 0; i < jeeps.Length; i++)
                {
                    if (jeeps[i].transform.position.z < -4.7f)
                    {
                        jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, Random.Range(speed - 4f, speed));
                    }
                }
                */
                materialXPlus.color = materialXMinus.color = Color.red;
            }
            else if (i % 4 == 3)
            {
                materialZPlus.color = materialZMinus.color = Color.yellow;
                materialXPlus.color = materialXMinus.color = Color.yellow;

                trafficBlockerPlaneZ.SetActive(true);
                trafficBlockerPlaneX.SetActive(true);
            }
            //prevTime = Time.time;
            nextTime = Time.time + 5f;
            i++;
        }
    }
}
