using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrafficLight : MonoBehaviour
{
    public Material materialZPlus, materialZMinus, materialXPlus, materialXMinus;
    private float nextTime = 0;//, prevTime;
    private int i = 0;

    public GameObject trafficLightZPlus, trafficLightZMinus, trafficLightXPlus, trafficLightXMinus;

    public GameObject trafficBlockerPlaneZ;
    public GameObject trafficBlockerPlaneX;

    private Rigidbody rbCurrent;
    private GameObject[] jeeps;
    private float speed = 10f;

    public Text vehicleZtext, vehicleXtext, timesText, timeElapsed;

    public float totalNumberOfCarsX = 0, totalNumberOfCarsZ = 0, totalTimeX = 0, totalTimeZ = 0;
    
    public Text meanZText, meanXText, meanTotal, densityZtext, countPassedXText, countPassedZText;

    public int countPassedX = 0, countPassedZ = 0;

    private int ctrZ, ctrX;

    private string density;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 2f;
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

        timeElapsed.text = ((float)((int)(Time.time * 1000.0f)) / 1000.0f).ToString();

        ctrZ = countVehicles('Z');
        ctrX = countVehicles('X');
        vehicleZtext.text = "Vehicles in lane 2 = " + ctrZ.ToString();
        vehicleXtext.text = "Vehicles in lane 1 = " + ctrX.ToString();


        if (Time.time > nextTime)
        {
            if(i % 4 == 0)
            {
                jeeps = GameObject.FindGameObjectsWithTag("JeepX");

                materialZPlus.color = materialZMinus.color  = Color.red;
                trafficBlockerPlaneZ.SetActive(true);
                trafficBlockerPlaneX.SetActive(false);

                for (int i = 0; i < jeeps.Length; i++)
                {
                    if (jeeps[i].transform.position.x < -4.7f)
                    {
                        jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(speed - 4f, speed), 0, 0);
                    }
                }

                materialXPlus.color = materialXMinus.color  = Color.green;
            }
            else if(i % 4 == 1)
            {
                materialZPlus.color = materialZMinus.color  = Color.yellow;
                materialXPlus.color = materialXMinus.color  = Color.yellow;

                trafficBlockerPlaneZ.SetActive(true);
                trafficBlockerPlaneX.SetActive(true);
            }
            else if(i % 4 == 2)
            {
                jeeps = GameObject.FindGameObjectsWithTag("Jeep");

                materialZPlus.color = materialZMinus.color  = Color.green;
                //delay????????
                trafficBlockerPlaneX.SetActive(true);
                trafficBlockerPlaneZ.SetActive(false);
                
                for (int i = 0; i < jeeps.Length; i++)
                {
                    if(jeeps[i].transform.position.z < -4.7f)
                    {
                        jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, UnityEngine.Random.Range(speed - 4f, speed));
                    }
                }

                materialXPlus.color = materialXMinus.color  = Color.red;
            }
            else if(i % 4 == 3)
            {
                materialZPlus.color = materialZMinus.color  = Color.yellow;
                materialXPlus.color = materialXMinus.color  = Color.yellow;

                trafficBlockerPlaneZ.SetActive(true);
                trafficBlockerPlaneX.SetActive(true);
            }
            //prevTime = Time.time;
            nextTime = Time.time + 5f;
            i += 2;
        }
    }

    public int countVehicles(char xz)
    {
        int count = 0;

        if (xz == 'Z')
        {
            GameObject[] gzJeeps = GameObject.FindGameObjectsWithTag("Jeep");
            for (int i = 0; i < gzJeeps.Length; i++)
            {
                //if (gzJeeps[i].GetComponent<Rigidbody>().velocity.z < 3f &&  - 35 < gzJeeps[i].transform.position.z && gzJeeps[i].transform.position.z < 5) //gzJeeps[i].transform.position.z < 5f)
                if (-30 < gzJeeps[i].transform.position.z && gzJeeps[i].transform.position.z < 5) //gzJeeps[i].transform.position.z < 5f)
                {
                    count++;
                }
            }

        }
        else if (xz == 'X')
        {
            GameObject[] gxJeeps = GameObject.FindGameObjectsWithTag("JeepX");
            for (int i = 0; i < gxJeeps.Length; i++)
            {
                //if (gxJeeps[i].GetComponent<Rigidbody>().velocity.x < 3f && -35 < gxJeeps[i].transform.position.z && gxJeeps[i].transform.position.x < 5)
                if (-30 < gxJeeps[i].transform.position.z && gxJeeps[i].transform.position.x < 5)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void getWaitingTime(float oneVehiclesTime, char xz)
    {
        float zNo = Single.Parse(countPassedZText.text.ToString());
        float xNo = Single.Parse(countPassedXText.text.ToString());
        if (xz == 'Z')
        {
            totalTimeZ += oneVehiclesTime;
            totalNumberOfCarsZ++;
            meanZText.text = /*"Mean Waiting Time in Lane 2 = " +*/ (totalTimeZ / totalNumberOfCarsZ).ToString();
            meanTotal.text = ( ( ( Single.Parse(meanZText.text.ToString()) *  zNo) + ( Single.Parse(meanXText.text.ToString()) * xNo) ) / (xNo + zNo) ).ToString();
        }
        else if (xz == 'X')
        {
            totalTimeX += oneVehiclesTime;
            totalNumberOfCarsX++;
            meanXText.text = /*"Mean Waiting Time in Lane 1 = " +*/ (totalTimeX / totalNumberOfCarsX).ToString();
            meanTotal.text = (((Single.Parse(meanZText.text.ToString()) * zNo) + (Single.Parse(meanXText.text.ToString()) * xNo)) / (xNo + zNo)).ToString();
        }
    }

    public void getCountPassed(char xz)
    {
        if (xz == 'Z')
        {
            countPassedZ++;
            countPassedZText.text = countPassedZ.ToString();
        }
        else if (xz == 'X')
        {
            countPassedX++;
            countPassedXText.text = countPassedX.ToString();
        }
    }

    public void getMessage(string data)
    {
        density = data;
        int.Parse("SDFg");
        //Debug.Log("density = " + data);
        //densityZtext.text = density;
    }

}
