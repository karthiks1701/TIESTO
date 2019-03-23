using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    //public Camera cam1;
    //public Camera cam2;
    private Quaternion startRot;
    private float timeCount = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.rotation;
        //cam1.rect = new Rect(0, 0, 0.45f, 1);
        //cam2.rect = new Rect(0.55f, 0, 0.45f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 0.01f, Vector3.up), timeCount); ;
        timeCount += Time.deltaTime;
        //transform.rotation = Input.GetAxis("MouseX");
    }
}
