using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkingPlayer : IState
{
    protected PlayerController playerController;
    protected Animator animator;

    public WalkingPlayer(PlayerController playerController, Animator animator)
    {
        this.playerController = playerController;
        this.animator = animator;
    }

    public void Enter()
    {
        playerController.isIdle = false;
        playerController.isWalking = true;
        playerController.isTalking = false;
        playerController.isFrozen = false;
        playerController.isSceneControlled = false;
        playerController.isDead = false;

        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", true);
        animator.SetBool("isHurt", false);
        animator.SetBool("isDead", false);
    }

    public void Execute()
    {
        if (playerController.CheckForPlayerMoveInput())
        {
            playerController.SetFacingDirection();
            playerController.MovePlayer();

            if (playerController.isLaserControlled)
            {
                animator.SetFloat("medusaWalkSpeedMultiplier", playerController.medusaLaserWalkSpeedMultiplier);

                if ((playerController.isFacingLeft && Input.GetAxisRaw("Horizontal") > 0) || (!playerController.isFacingLeft && Input.GetAxisRaw("Horizontal") < 0))
                {
                    animator.SetBool("isWalkBack", true);
                } 
            }
            else
            {
                animator.SetFloat("medusaWalkSpeedMultiplier", 1);
                animator.SetBool("isWalkBack", false);
            }
                

        }
        if(!playerController.CheckForPlayerMoveInput() || (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0))
        {
            playerController.ChangeStateToIdle();
        }
        
    }

    public void Exit()
    {
        playerController.playerRigidBody.velocity = Vector2.zero;
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkBack", false);
        animator.SetFloat("medusaWalkSpeedMultiplier", 1);
    }
}