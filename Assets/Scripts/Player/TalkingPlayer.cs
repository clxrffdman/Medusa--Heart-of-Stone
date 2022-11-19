using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TalkingPlayer : IState
{
    protected PlayerController playerController;
    protected Animator animator;
    
    public TalkingPlayer(PlayerController playerController, Animator animator)
    {
        this.animator = animator;
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.isIdle = false;
        playerController.isWalking = false;
        playerController.isTalking = true;
        playerController.isFrozen = false;

        animator.SetBool("isIdle", true);
        playerController.playerRigidBody.velocity = Vector2.zero;
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}

