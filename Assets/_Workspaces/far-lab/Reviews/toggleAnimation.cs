using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleAnimation : MonoBehaviour {
	public bool RunAnnimtation = false;

	public Vector3 smallSize;
	public Vector3 fullSize;
	// Use this for initialization
	void Start () {


		foreach (SignAnimation a in FindObjectsOfType<SignAnimation>()) {
			if (RunAnnimtation){
				a.transform.localScale = smallSize;
				a.enabled = true;
			}else{
				a.transform.localScale = fullSize;
				a.enabled = false;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
