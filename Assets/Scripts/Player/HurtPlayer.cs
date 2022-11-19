using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HurtPlayer : IState
{
    private PlayerController playerController;
    private Animator animator;

    public HurtPlayer(PlayerController playerController, Animator animator)
    {
        this.playerController = playerController;
        this.animator = animator;
    }

    public void Enter()
    {
        playerController.isIdle = true;
        playerController.isWalking = false;
        playerController.isTalking = false;
        playerController.isFrozen = false;
        playerController.isSceneControlled = false;
        playerController.isDead = false;
        playerController.PlayHurtSfx();
        playerController.laserEnabled = false;

        playerController.playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkBack", false);
        animator.SetBool("isHurt", true);
        animator.SetBool("isDead", false);
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        playerController.laserEnabled = true;
        animator.SetBool("isHurt", false);
    }

    
}
