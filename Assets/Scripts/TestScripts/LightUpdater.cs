using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightUpdater : MonoBehaviour
{
    public Light2D light2D;
    public float lightIntensityOnStart;

    void Start()
    {
        light2D.intensity = lightIntensityOnStart;
    }

}
