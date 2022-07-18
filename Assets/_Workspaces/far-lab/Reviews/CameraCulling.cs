using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCulling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float[] distances = new float[32];
		distances[19] = 100;
		Camera.main.layerCullDistances = distances;
	}

}
