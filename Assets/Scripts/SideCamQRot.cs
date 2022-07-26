using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCamQRot : MonoBehaviour
{
    public GameObject car;
    public Vector3 offset;
    Quaternion offsetRotation;

    void Start ()
    {
        offset = transform.position - car.transform.position;

        offsetRotation = transform.rotation * Quaternion.Inverse(car.transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, car.transform.position + offset, 0.8f);

        transform.rotation = Quaternion.Slerp(transform.rotation, car.transform.rotation * offsetRotation, 0.8f);
    }
}
