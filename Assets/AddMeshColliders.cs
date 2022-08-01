using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddMeshColliders : MonoBehaviour
{
    
    //public List<GameObject> listOfChildren;
    private void GetChildren(GameObject obj)
    {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child 
            if (child.gameObject.GetComponent<MeshFilter>().mesh == null)
            {
                return;
            }

            else
            {
                Mesh mesh = child.gameObject.GetComponent<MeshFilter>().mesh;
                // Add a new MeshCollider to the child object
                MeshCollider meshCollider = child.gameObject.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = mesh;

                //listOfChildren.Add(child.gameObject);
                GetChildren(child.gameObject);
            }
        }
    }


    void Start()
    {
      GetChildren(this.GameObject());
    }

}
