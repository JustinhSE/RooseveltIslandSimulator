using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * (speed * Time.deltaTime));
        if (Input.GetKey(KeyCode.X))
            transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Z))
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
    }
}
