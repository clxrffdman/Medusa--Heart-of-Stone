using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeadPlayer : IState
{
    private PlayerController playerController;
    private Animator animator;

    public DeadPlayer(PlayerController playerController, Animator animator)
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
        playerController.isDead = true;

        playerController.PlayHurtSfx();
        playerController.playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkBack", false);
        animator.SetBool("isHurt", false);
        animator.SetBool("isDead", true);

        playerController.PrintString("is dead now");
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}

