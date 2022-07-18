using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
    Transform player;

    void Start(){
        player = FindObjectOfType<VehicleController>().transform;
    }
	// Update is called once per frame
	void Update() 
	{
       // Debug.DrawLine(transform.position, player.position);
       // Debug.Log(player.position);
		transform.LookAt(player, Vector3.up);
	}
} 
