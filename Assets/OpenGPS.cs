using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGPS : MonoBehaviour{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.M))
		{
			transform.GetChild(0).gameObject.SetActive(true);
		}
	}
}
