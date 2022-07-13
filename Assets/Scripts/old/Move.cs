using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTranslate : MonoBehaviour

{
    // Start is called before the first frame update
    private void update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
