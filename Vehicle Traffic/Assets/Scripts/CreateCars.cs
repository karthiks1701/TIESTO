using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCars : MonoBehaviour
{

    public GameObject jeep, jeepX;
    private float nextTime1 = 0.3f, nextTime2 = 0.3f;
    public Transform vehiclesParentX, vehiclesParentZ;

    private static int densityZ = 50000, densityX = 50000;
    private static int x = 0, z = 0;

    private bool done = false;

    //public HelloRequester helloRequester;

    private static int[] a;
    private int msg1, msg2;

    public GameObject policeCar;

    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        

        /*
        msg1 = HelloRequester.getMessage1();
        Debug.Log(msg1 + " Lane 1");
        densityX = int.Parse(msg1);

        msg2 = HelloRequester.getMessage2();
        Debug.Log(msg2 + "  Lane 2 ");
        densityZ = int.Parse(msg2);
        */

        //Debug.Log(helloRequester.message);
        //densityX = int.Parse(helloRequester.message);
        //Debug.Log(helloRequester.gotMessage);
        /*
        if (helloRequester.message != prev)
        {
            Debug.Log(helloRequester.message);
            densityX = int.Parse(helloRequester.message);
            prev = helloRequester.message;
        }
        */
        GameObject[] jeeps = GameObject.FindGameObjectsWithTag("Jeep");
        GameObject[] jeepXs = GameObject.FindGameObjectsWithTag("JeepX");

        if (z < densityZ && Time.time > nextTime1)
        {
            bool flag1 = true;
            Vector3 pos1 = new Vector3(Random.Range(-2, +2), 0.6f, Random.Range(-70, -40)); //(-40, 0.6f, 0);
            //To check if two vehicles are too close!
            for (int i = 0; i < jeeps.Length; i++)
            {
                if (Mathf.Abs(pos1.x - jeeps[i].transform.position.x) <= 1.12f && Mathf.Abs(pos1.z - jeeps[i].transform.position.z) <= 2.69f)
                {
                    flag1 = false;
                    z--;
                    break;
                }
            }
            if (flag1)
            {
                
                /*
                if (counter % 40 == 0)
                {
                    GameObject policeCarCurrent = Instantiate(policeCar, pos1, Quaternion.identity, vehiclesParentZ);
                    policeCarCurrent.GetComponent<VehicleMovement>().enabled = true;
                    policeCarCurrent.GetComponent<VehicleMovementX>().enabled = false;
                }
                else
                {*/
                    Instantiate(jeep, pos1, Quaternion.identity, vehiclesParentZ);
                //}

                nextTime1 += Random.Range(0.5f, 1.5f);  
                counter++;
            }
            z++;
        }
        if (x < densityX && Time.time > nextTime2)
        {
            bool flag2 = true;
            Vector3 pos2 = new Vector3(Random.Range(-70, -40), 0.6f, Random.Range(-2, +2)); //(-40, 0.6f, 0);
            //To check if two vehicles are too close!
            for (int i = 0; i < jeepXs.Length; i++)
            {
                if (Mathf.Abs(pos2.x - jeepXs[i].transform.position.x) <= 1.12f && Mathf.Abs(pos2.z - jeepXs[i].transform.position.z) <= 2.69f)
                {
                    flag2 = false;
                    x--; 
                    break;
                }
            }
            if (flag2)
            {
                
                /*
                if(counter % 40 == 0)
                {
                    GameObject policeCarCurrent = Instantiate(policeCar, pos2, Quaternion.Euler(0, 90, 0), vehiclesParentX);
                    policeCarCurrent.GetComponent<VehicleMovementX>().enabled = true;
                    policeCarCurrent.GetComponent<VehicleMovement>().enabled = false;
                }
                else
                {*/
                
                    Instantiate(jeepX, pos2, Quaternion.Euler(0, 90, 0), vehiclesParentX);
                //}
                nextTime2 += Random.Range(0.5f, 1.5f); // 0.5f;
                counter++;
            }
            x++;
        }



        /*
        if (Time.time > nextTime1)
        {
            //get gameObject from Instantiate to check if two vehicles are too close!!!!!!!!!!!!!!! 
            Instantiate(jeep, new Vector3(Random.Range(-2, +2), 0.6f, -70), Quaternion.identity, vehiclesParentZ);
            nextTime1 += Random.Range(3f, 7f);
        }
        if (Time.time > nextTime2)
        {
            //get gameObject from Instantiate to check if two vehicles are too close!!!!!!!!!!!!!!! 
            Instantiate(jeepX, new Vector3(-70, 0.6f, Random.Range(-2, +2)), Quaternion.Euler(0, 90, 0), vehiclesParentX);
            nextTime2 += Random.Range(0.4f, 2f);
        }
        */
    }


    public static void getTheMessage(int[] a)
    {
        CreateCars.a = a;
        densityX = a[0];
        densityZ = a[1];
        CreateCars.x = CreateCars.z = 0;
        Debug.Log("densityX = " + densityX + " densityZ = " + densityZ);
    }

}
