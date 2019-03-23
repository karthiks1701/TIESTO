using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using UnityEngine.SceneManagement;

public class TrafficLightAI : MonoBehaviour
{
    public Material materialZPlus, materialZMinus, materialXPlus, materialXMinus;
    private float nextTime = 4, prevTime = 0, timeIncr = 5f;
    private int i = 0;

    public GameObject trafficLightZPlus, trafficLightZMinus, trafficLightXPlus, trafficLightXMinus;

    public GameObject trafficBlockerPlaneZ;
    public GameObject trafficBlockerPlaneX;

    private Rigidbody rbCurrent;
    private GameObject[] jeeps;
    private float speed = 10f;
    //private bool flag = false;

    public Text vehicleZtext, vehicleXtext, timesText, timeElapsed;

    private int ctrZ, ctrX;

    private string density;

    public bool redFlag = false, greenFlag = false;

    //private bool done = false, done1 = false, done2 = false, done3 = false;

    public float totalNumberOfCarsX = 0, totalNumberOfCarsZ = 0, totalTimeX = 0, totalTimeZ = 0;

    public Text meanZText, meanXText, meanTotal, densityZtext, countPassedXText, countPassedZText;

    public int countPassedX = 0, countPassedZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        Time.timeScale = 2f;
        trafficBlockerPlaneZ.SetActive(false);
        trafficBlockerPlaneX.SetActive(true);
        materialZPlus = trafficLightZPlus.GetComponent<Renderer>().material;
        materialZMinus = trafficLightZMinus.GetComponent<Renderer>().material;
        materialXPlus = trafficLightXPlus.GetComponent<Renderer>().material;
        materialXMinus = trafficLightXMinus.GetComponent<Renderer>().material;

        //---------//---------//---------//---------//---------//

        //Yellow
        materialZPlus.color = materialZMinus.color = Color.yellow;
        materialXPlus.color = materialXMinus.color = Color.yellow;

        trafficBlockerPlaneZ.SetActive(true);
        trafficBlockerPlaneX.SetActive(true);
        //nextTime = 13f;

    }

    private void Update()
    {
        timeElapsed.text = ((float)((int)(Time.time * 1000.0f)) / 1000.0f).ToString();
        Debug.Log(GameObject.FindGameObjectWithTag("ESV"));
        if(GameObject.FindGameObjectWithTag("ESV") != null && GameObject.FindGameObjectWithTag("ESV").transform.position.x < 0)
        {
            //--------------------------------- Make Z RED ---------------------------------
            jeeps = GameObject.FindGameObjectsWithTag("JeepX");

            materialZPlus.color = materialZMinus.color = Color.red;
            trafficBlockerPlaneZ.SetActive(true);
            trafficBlockerPlaneX.SetActive(false);


            GameObject.FindGameObjectWithTag("ESV").GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(speed - 4f, speed), 0, 0);
            for (int i = 0; i < jeeps.Length; i++)
            {
                if (jeeps[i].transform.position.x < -4.7f)
                {
                    jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(speed - 4f, speed), 0, 0);
                }
            }

            materialXPlus.color = materialXMinus.color = Color.green;
        }
        else if(GameObject.FindGameObjectWithTag("ESV") != null && GameObject.FindGameObjectWithTag("ESV").transform.position.z < 0)
        {
            //--------------------------------- Make Z GREEN ---------------------------------
            jeeps = GameObject.FindGameObjectsWithTag("Jeep");

            materialZPlus.color = materialZMinus.color = Color.green;
            //delay????????
            trafficBlockerPlaneX.SetActive(true);
            trafficBlockerPlaneZ.SetActive(false);


            GameObject.FindGameObjectWithTag("ESV").GetComponent<Rigidbody>().velocity = new Vector3(0, 0, UnityEngine.Random.Range(speed - 4f, speed));
            for (int i = 0; i < jeeps.Length; i++)
            {
                if (jeeps[i].transform.position.z < -4.7f)
                {
                    jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, UnityEngine.Random.Range(speed - 4f, speed));
                }
            }

            materialXPlus.color = materialXMinus.color = Color.red;
        }



        else
        {

            ctrZ = countVehicles('Z');
            ctrX = countVehicles('X');
            vehicleZtext.text = "Vehicles in lane 2 = " + ctrZ.ToString();
            vehicleXtext.text = "Vehicles in lane 1 = " + ctrX.ToString();


            if (Time.time > nextTime)
            {
                //Debug.Log("i = " + i);
                if (i % 4 == 0)    //--------------------------------- Make Z RED ---------------------------------
                {
                    //timesText.text += "\n" + (Time.time - prevTime).ToString();


                    prevTime = Time.time;
                    //Debug.Log(Time.time);

                    //--------------------------------- Make Z RED ---------------------------------
                    jeeps = GameObject.FindGameObjectsWithTag("JeepX");

                    materialZPlus.color = materialZMinus.color = Color.red;
                    trafficBlockerPlaneZ.SetActive(true);
                    trafficBlockerPlaneX.SetActive(false);

                    for (int i = 0; i < jeeps.Length; i++)
                    {
                        if (jeeps[i].transform.position.x < -4.7f)
                        {
                            jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(UnityEngine.Random.Range(speed - 4f, speed), 0, 0);
                        }
                    }

                    materialXPlus.color = materialXMinus.color = Color.green;

                    //Debug.Log(i);  //////////////////
                    float[] a = checker();
                    i = (int)a[0];
                    timeIncr = a[1];
                    //Debug.Log("to ---" + (i) + "a[1] = " + a[1]);///////////////////////////

                    //i = checker() - 1 /////////////////////////////////////////////////
                    //Debug.Log("to ---" + (i + 1));///////////////////////////

                    //flag = false;

                    redFlag = true;
                    greenFlag = false;

                }
                else if (i % 4 == 1)   //-------------- YELLOW --------------
                {
                    //Debug.Log(Time.time);
                    //Yellow
                    materialZPlus.color = materialZMinus.color = Color.yellow;
                    materialXPlus.color = materialXMinus.color = Color.yellow;

                    trafficBlockerPlaneZ.SetActive(true);
                    trafficBlockerPlaneX.SetActive(true);

                    //flag = true;
                }
                else if (i % 4 == 2)    //--------------------------------- Make Z GREEN ---------------------------------
                {
                    prevTime = Time.time;
                    //Debug.Log(Time.time);

                    //--------------------------------- Make Z GREEN ---------------------------------
                    jeeps = GameObject.FindGameObjectsWithTag("Jeep");

                    materialZPlus.color = materialZMinus.color = Color.green;
                    //delay????????
                    trafficBlockerPlaneX.SetActive(true);
                    trafficBlockerPlaneZ.SetActive(false);

                    for (int i = 0; i < jeeps.Length; i++)
                    {
                        if (jeeps[i].transform.position.z < -4.7f)
                        {
                            jeeps[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, UnityEngine.Random.Range(speed - 4f, speed));
                        }
                    }

                    materialXPlus.color = materialXMinus.color = Color.red;

                    //flag = false;

                    //Debug.Log(i); ///////////////////////////
                    float[] a = checker();
                    i = (int)a[0];
                    timeIncr = a[1];
                    //Debug.Log("to ---" + (i) + "a[1] = " + a[1]);///////////////////////////

                    //i = checker() - 1 /////////////////////////////////////////////////
                    //Debug.Log("to ---" + (i + 1));///////////////////////////

                    greenFlag = true;
                    redFlag = false;

                }
                else if (i % 4 == 3)  //-------------- YELLOW --------------
                {
                    //Debug.Log(Time.time);
                    //Yellow
                    materialZPlus.color = materialZMinus.color = Color.yellow;
                    materialXPlus.color = materialXMinus.color = Color.yellow;

                    trafficBlockerPlaneZ.SetActive(true);
                    trafficBlockerPlaneX.SetActive(true);

                    //flag = true;
                }
                //prevTime = Time.time;
                nextTime = Time.time + timeIncr;
                i++;
            }

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

    public float[] checker()
    {
        float timeIncr1 = 2f;
        int i = -1;   
        if (ctrZ > ctrX)
        {
            if (ctrX <= 3)
            {
                i = 1;    // -------------- Make Z GREEN --------------
            }
            else if (ctrZ - ctrX > 4)
            {
                //if (ctrX < 3)
                {
                    i = 1; // -------------- Make Z GREEN --------------
                    //timeIncr1 = 2f;
                }
            }
            else
            {
                i = 3;    // -------------- Make Z RED --------------
            }
        }
        else if (ctrZ < ctrX)
        {
            if (ctrZ <= 3)
            {
                i = 3;     // -------------- Make Z RED --------------
            }
            else if (ctrX - ctrZ > 4)
            {
                //if (ctrZ < 3)
                {
                    i = 3;  // -------------- Make Z RED --------------
                    //timeIncr1 = 2f;
                }
            }
            else
            {
                i = 1;   // -------------- Make Z GREEN --------------
                //Debug.Log("nooo " + ctrZ + " " + ctrX); 
            }
        }
        //else if (ctrZ == ctrX)// && materialZPlus.color.Equals(Color.yellow))
        //{
        //    i = 1;  // -------------- Make Z GREEN --------------
        //}
        //Debug.Log("i ====== " + i);
       float[] a = {i, timeIncr1};
       return a;
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
            meanTotal.text = (((Single.Parse(meanZText.text.ToString()) * zNo) + (Single.Parse(meanXText.text.ToString()) * xNo)) / (xNo + zNo)).ToString();
        }
        else if(xz == 'X')
        {
            totalTimeX += oneVehiclesTime;
            totalNumberOfCarsX++;
            meanXText.text = /*"Mean Waiting Time in Lane 1 = " +*/ (totalTimeX / totalNumberOfCarsX).ToString();
            meanTotal.text = (((Single.Parse(meanZText.text.ToString()) * zNo) + (Single.Parse(meanXText.text.ToString()) * xNo)) / (xNo + zNo)).ToString();
        }
    }

    public void getCountPassed(char xz)
    {
        if(xz == 'Z')
        {
            countPassedZ++;
            countPassedZText.text = countPassedZ.ToString(); 
        }
        else if(xz == 'X')
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
