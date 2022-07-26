using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    Camera cam;
    public int width = 256;
    public int height = 256;
    public bool flipHorizontal;

    void Start()
    {
        cam = GetComponent<Camera>();
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    void OnPreCull()
    {
        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        Vector3 scale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(scale);
    }

    void OnPreRender() { GL.invertCulling = flipHorizontal; }

    void OnPostRender() { GL.invertCulling = false; }
}
