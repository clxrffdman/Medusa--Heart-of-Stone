using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrozenPlayer : IState
{
    protected PlayerController playerController;
    protected Animator animator;

    public FrozenPlayer(PlayerController playerController, Animator animator)
    {
        this.playerController = playerController;
        this.animator = animator;
    }

    public void Enter()
    {
        playerController.isIdle = false;
        playerController.isWalking = false;
        playerController.isTalking = false;
        playerController.isFrozen = true;
        playerController.isSceneControlled = false;
        playerController.isDead = false;

        playerController.playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkBack", false);
        animator.SetBool("isHurt", false);
        animator.SetBool("isDead", false);
    }

    public void Execute()
    {
       
    }

    public void Exit()
    {

    }
}