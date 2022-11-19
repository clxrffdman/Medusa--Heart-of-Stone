using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightEffector : MonoBehaviour
{
    private Light2D light2D;
    public LayerMask includePlayer;
    public LayerMask excludePlayer;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
        }
    }
}
