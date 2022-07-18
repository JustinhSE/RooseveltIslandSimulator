using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {
    Vector3 randomRotation;
	// Use this for initialization
	void Start () {
        randomRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = transform.rotation* Quaternion.AngleAxis(Time.deltaTime *45, randomRotation.normalized);
	}
    public void changeRotation() {
        Vector3 temp= new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
      
        while (temp == randomRotation)
        {
            temp = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        randomRotation = temp;
    }
}
