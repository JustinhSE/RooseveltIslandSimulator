using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddMeshColliders : MonoBehaviour
{
    void Start()
    {
        //GetChildren(this.GameObject());
        addMeshes(this.GameObject());
    }
    
    private void addMeshes(GameObject obj)
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.GetComponent<MeshFilter>() != null)
            {
                child.gameObject.AddComponent<MeshCollider>();
            }
        }
    }
}


