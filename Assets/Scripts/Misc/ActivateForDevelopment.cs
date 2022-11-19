using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateForDevelopment : MonoBehaviour
{
    void Start()
    {
        if (!GameManager.Instance.isInDevelopmentMode)
        {
            this.gameObject.SetActive(false);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
