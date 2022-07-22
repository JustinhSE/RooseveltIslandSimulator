using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invertview : MonoBehaviour
{
    void OnPreCull () {
        Matrix4x4 scale;
        if(GetComponent<Camera>().aspect >2){
            scale = Matrix4x4.Scale (new Vector3 (-1, 1, 1));
        }else{
            scale = Matrix4x4.Scale (new Vector3 (1, -1, 1));
        }
        GetComponent<Camera>().ResetWorldToCameraMatrix ();
        GetComponent<Camera>().ResetProjectionMatrix ();
        GetComponent<Camera>().projectionMatrix = GetComponent<Camera>().projectionMatrix * scale;
    }
    void OnPreRender () {
        GL.SetRevertBackfacing (true);
    }
    void OnPostRender () {
        GL.SetRevertBackfacing (false);
    }
}

