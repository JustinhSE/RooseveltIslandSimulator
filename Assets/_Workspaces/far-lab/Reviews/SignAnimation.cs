using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignAnimation : MonoBehaviour {

	Transform player;

	private float TimeScale = 0.5f;
	private Vector3 InitialScale;
	private Vector3 FinalScale;
	private float progress;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<VehicleController>().transform;
		InitialScale = new Vector3(.0003f, .0002f, .00001f);
		FinalScale = new Vector3(3.0f, 2.0f, 0.1f);
		progress = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(player.position, transform.position);
		if (dist < 50) {
			if (progress < 1) {
				transform.localScale = Vector3.Lerp(transform.localScale, FinalScale, progress);
				progress += Time.deltaTime * TimeScale;
			}
		}

	}
}
