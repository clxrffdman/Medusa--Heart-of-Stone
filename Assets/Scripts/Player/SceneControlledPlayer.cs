using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneControlledPlayer : IState
{
    private PlayerController playerController;
    private Animator animator;

    public SceneControlledPlayer(PlayerController playerController, Animator animator)
    {
        this.playerController = playerController;
        this.animator = animator;
    }

    public void Enter()
    {
        playerController.isIdle = false;
        playerController.isWalking = false;
        playerController.isTalking = false;
        playerController.isFrozen = false;
        playerController.isSceneControlled = true;
        playerController.isDead = false;

        if (playerController.targetDestination != null)
        {
            
            playerController.SetAStarDestination(playerController.targetDestination);
            playerController.StartAStarPathfinding();
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isWalkBack", false);
            animator.SetBool("isHurt", false);
            animator.SetBool("isDead", false);
        }
    }

    public void Execute()
    {
        playerController.SetFacingDirection();
        if (playerController.CheckIfEndReached())
        {
            playerController.ChangeStateToFrozen();
        }
    }

    public void Exit()
    {
        if (playerController.targetDestination != null)
        {
            playerController.StopAStarPathfinding();
        }
    }
}