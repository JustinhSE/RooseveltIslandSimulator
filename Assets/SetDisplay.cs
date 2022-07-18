using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Canvas elmo in GetComponentsInChildren<Canvas>())
        {

            elmo.targetDisplay = 1;

        }
        ActivateAllDisplays(2); 
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActivateAllDisplays(int max = 8)
    {
        for (int num = 0; num < max; num++)
        {
            if (Display.displays.Length > num)
            {
                Display.displays[num].Activate();
            }
        }

    }
}
