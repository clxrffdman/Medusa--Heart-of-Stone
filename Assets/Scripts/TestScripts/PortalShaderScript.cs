using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalShaderScript : MonoBehaviour
{

    public SpriteRenderer sr;
    public Texture destinationImage;


    // Start is called before the first frame update
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        sr.material.SetTexture("_DestinationTex", destinationImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
