using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCarsNormal : MonoBehaviour
{

    public GameObject jeepModelNormal, jeepXModelNormal;
    private float nextTime1 = 0.3f, nextTime2 = 0.3f;
    public Transform vehiclesParentX, vehiclesParentZ;

    private int densityZ = 50000, densityX = 50000, x = 0, z = 0;

    private bool done = false;

    private Transform road;

    // Start is called before the first frame update
    void Start()
    {
        road = GameObject.Find("Road").transform;
        //Instantiate(jeepModelNormal, new Vector3(0, 0, 5), Quaternion.identity);
        //Instantiate(jeepModelNormal, new Vector3( 0, 0, 30), Quaternion.identity, transform);
        //jeepPrev = Instantiate(jeepModelNormal, new Vector3(Random.value * 4 - 2, 0.5f, -30), Quaternion.identity, vehiclesParent);
    }

    // Update is called once per frame
    void Update()
    {

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
                Instantiate(jeepModelNormal, pos1, Quaternion.identity, vehiclesParentZ);
                nextTime1 += Random.Range(0.2f, 2f);   //4f;
            }
            z++;
        }
        if (x < densityX && Time.time > nextTime2)
        {
            bool flag2 = true;
            Vector3 pos2 = new Vector3(Random.Range(-70, -40), 0.6f, Random.Range(-2, +2)); // (-40, 0.6f, 0);
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
                Instantiate(jeepXModelNormal, pos2, Quaternion.Euler(0, 90, 0), vehiclesParentX);
                nextTime2 += Random.Range(0.2f, 2f); //0.5f;
            }
            x++;
        }



        /*
        if (Time.time > nextTime1)
        {
            //get gameObject from Instantiate to check if two vehicles are too close!!!!!!!!!!!!!!! 
            Instantiate(jeepModelNormal, new Vector3(Random.Range(-2, +2), 0.6f, -70), Quaternion.identity, vehiclesParentZ);
            nextTime1 += Random.Range(3f, 7f);
        }
        if (Time.time > nextTime2)
        {
            //get gameObject from Instantiate to check if two vehicles are too close!!!!!!!!!!!!!!! 
            Instantiate(jeepXModelNormal, new Vector3(-70, 0.6f, Random.Range(-2, +2)), Quaternion.Euler(0, 90, 0), vehiclesParentX);
            nextTime2 += Random.Range(0.4f, 2f);
        }
        */
    }
}
