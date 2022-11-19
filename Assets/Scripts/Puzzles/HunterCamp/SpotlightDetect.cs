using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightDetect : MonoBehaviour
{

    [SerializeField] private Animator spotLightAnimator;
    public bool spotted;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHurtbox")
        {
            spotted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHurtbox")
        {
            spotted = false;
        }
    }

    public void StopSpotlightMovement()
    {
        spotLightAnimator.speed = 0;
    }
}
