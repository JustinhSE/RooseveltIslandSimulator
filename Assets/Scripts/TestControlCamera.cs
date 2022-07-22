using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControlCamera : MonoBehaviour
{
    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * ( Time.deltaTime));
        if (Input.GetKey(KeyCode.X))
            transform.Rotate(Vector3.up  * Time.deltaTime);
            

        if (Input.GetKey(KeyCode.Z))
            transform.Rotate(-Vector3.up * Time.deltaTime);
    }
}

