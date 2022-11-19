using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScriptInstance : MonoBehaviour
{

    public SpriteRenderer sr;
    public float visibilityValue;
    public float windStrengthVariation;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material.SetFloat("_Visibility", visibilityValue);
        sr.material.SetFloat("_windStrength", sr.material.GetFloat("_windStrength") + Random.Range(-windStrengthVariation, windStrengthVariation));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
