using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.C))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
	}
}
