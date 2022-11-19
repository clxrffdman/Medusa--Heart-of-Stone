using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdlePlayer : IState
{
    private PlayerController playerController;
    private Animator animator;
  
    public IdlePlayer(PlayerController playerController, Animator animator)
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
        

        playerController.playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkBack", false);
        animator.SetBool("isHurt", false);
        animator.SetBool("isDead", false);
    }

    public void Execute()
    {

        playerController.SetFacingDirection();

        if (playerController.CheckForPlayerMoveInput())
        {
            playerController.ChangeStateToWalking();
        }
    }

    public void Exit()
    {
   
    }
}
