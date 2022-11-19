using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierYCheck : MonoBehaviour
{
    public bool validY;
    public LayerMask playerMask;
    public float checkDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector3.right, checkDistance, playerMask))
        {
            validY = true;
        }
        else
        {
            validY = false;
        }

    }
}
