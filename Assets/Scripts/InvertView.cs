using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertView : MonoBehaviour
{
    // Start is called before the first frame update
    Camera backcam;

    void Start()
    {
        backcam = GetComponent<Camera>();
    }

    void OnPreCull()
    {
        backcam.ResetWorldToCameraMatrix();
        backcam.ResetProjectionMatrix();
        backcam.projectionMatrix = backcam.projectionMatrix * Matrix4x4.Scale(new Vector3(1, -1, 1));
    }

    void OnPreRender()
    {
        GL.invertCulling = true;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
