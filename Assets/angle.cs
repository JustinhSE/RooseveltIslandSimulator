using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angle : MonoBehaviour
{

    public Camera rightCamera;
    public Camera leftCamera;
    public Camera backCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceRight = Vector3.Distance(rightCamera.transform.position, backCamera.transform.position);

        //0.61 is this distance
        Debug.Log("this distance: " + distanceRight);


        float distanceLeft = Vector3.Distance(leftCamera.transform.position, backCamera.transform.position);
        //0.85 is this distance
        Debug.Log("this distance: " + distanceLeft);

    }
}
