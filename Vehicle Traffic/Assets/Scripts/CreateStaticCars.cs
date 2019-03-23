using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStaticCars : MonoBehaviour
{

    public GameObject jeep, jeepX;
    private float nextTime1, nextTime2;
    public Transform vehiclesParentX, vehiclesParentZ;

    public Text densityZtext = null;

    private int densityZ = 99, densityX = 99, x = 0, z = 0;

    // Start is called before the first frame update
    void Start()
    {
        //////--------get densities from HelloRequesyter
        //Instantiate(jeep, new Vector3(0, 0, 5), Quaternion.identity);
        //Instantiate(jeep, new Vector3( 0, 0, 30), Quaternion.identity, transform);
        //jeepPrev = Instantiate(jeep, new Vector3(Random.value * 4 - 2, 0.5f, -30), Quaternion.identity, vehiclesParent);
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] jeeps = GameObject.FindGameObjectsWithTag("Jeep");
        GameObject[] jeepXs = GameObject.FindGameObjectsWithTag("JeepX");

        if (z < densityZ && Time.time > nextTime1)
        {
            bool flag1 = true;
            Vector3 pos1 = new Vector3(Random.Range(-2, +2), 0.6f, Random.Range(-70, -40));
            //To check if two vehicles are too close!
            for (int i = 0; i < jeeps.Length; i++)
            {
                if(Mathf.Abs(pos1.x - jeeps[i].transform.position.x) <= 1.12f && Mathf.Abs(pos1.z - jeeps[i].transform.position.z) <= 2.69f)
                {
                    flag1 = false;
                    z--;
                    break;
                }
            }
            if (flag1)
            {
                Instantiate(jeep, pos1, Quaternion.identity, vehiclesParentZ);
                nextTime1 += Random.Range(0.4f, 2f);
            }
            z++;
        }
        if (x < densityX && Time.time > nextTime2)
        {
            bool flag2 = true;
            Vector3 pos2 = new Vector3(Random.Range(-70, -40), 0.6f, Random.Range(-2, +2));
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
                Instantiate(jeepX, pos2, Quaternion.Euler(0, 90, 0), vehiclesParentX);
                nextTime2 += Random.Range(0.4f, 2f);
            }
        }
        x++;
    }

    public void getMessage(int data)
    {
        densityX = data;
        densityZtext.text = data.ToString();
    }

}
