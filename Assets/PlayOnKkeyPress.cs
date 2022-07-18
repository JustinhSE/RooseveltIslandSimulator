using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnKkeyPress : MonoBehaviour {

    public AudioSource startingSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            startingSound.Play();
        }
		
	}
}
