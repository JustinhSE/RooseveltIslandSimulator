using UnityEngine;

public class MainStreetFlood : MonoBehaviour
{
    public float rate = 0.1f;
    private void Update()
    {
        transform.localScale += Vector3.one * (rate*Time.deltaTime);
    }
}
