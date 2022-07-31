using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMeshColliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> objects = GetAllObjectsInScene();
        foreach (GameObject childObject in objects)
        {
            if (childObject.gameObject.GetComponent<MeshFilter>().mesh != null)
            {
                Mesh mesh = childObject.gameObject.GetComponent<MeshFilter>().mesh;
                // Add a new MeshCollider to the child object
                MeshCollider meshCollider = childObject.gameObject.AddComponent<MeshCollider>();
            }

        }
    }

    private static List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.hideFlags != HideFlags.None)
                continue;
            objectsInScene.Add(go);
        }
        return objectsInScene;
    }

}
