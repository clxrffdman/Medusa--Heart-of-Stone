using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShaderTest : MonoBehaviour
{
    public GameObject targetLight;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.material.SetVector("_objectPos", transform.position);
        sr.material.SetVector("_lightPos", targetLight.transform.position);
    }
}
