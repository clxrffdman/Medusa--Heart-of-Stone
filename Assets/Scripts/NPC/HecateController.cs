using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HecateController : MonoBehaviour
{
    private Animator animator;

    public string currentState;
    public enum ObjectStates { isHappy, isSad, isAngry };

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimatorToHappy()
    {
        if(currentState != ObjectStates.isHappy.ToString())
        {
            currentState = ObjectStates.isHappy.ToString();
            animator.SetBool("isTransitioning", true);
            animator.SetBool("isHappy", true);
            animator.SetBool("isSad", false);
            animator.SetBool("isAngry", false);
        }
    }

    public void SetAnimatorToSad()
    {
        if (currentState != ObjectStates.isSad.ToString())
        {
            currentState = ObjectStates.isSad.ToString();
            animator.SetBool("isTransitioning", true);
            animator.SetBool("isHappy", false);
            animator.SetBool("isSad", true);
            animator.SetBool("isAngry", false);
        }
    }

    public void SetAnimatorToAngry()
    {
        if (currentState != ObjectStates.isAngry.ToString())
        {
            currentState = ObjectStates.isAngry.ToString();
            animator.SetBool("isTransitioning", true);
            animator.SetBool("isHappy", false);
            animator.SetBool("isSad", false);
            animator.SetBool("isAngry", true);
        }
    }

    public void StopSwitchTransition()
    {
        animator.SetBool("isTransitioning", false);
    }
}
