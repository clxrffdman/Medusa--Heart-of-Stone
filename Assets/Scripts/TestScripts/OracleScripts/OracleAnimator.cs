
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OracleAnimator : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;

    private Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimaton;
    private bool isTalkPressed;
    private bool isTalking;
    private bool is_SceneControlled;


    //Animation States
    const string ORACLE_IDLE = "Oracle_idle";
    const string ORACLE_WALK = "Oracle_walk";
    const string ORACLE_TALK = "Oracle_talk";
    const string ORACLE_SCENECONTROLLED = "Oracle_scenecontrolled";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    void Update()
    {
        //Checking for inputs
        xAxis = Input.GetAxisRaw("Horizontal");

        //E talk key pressed?
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTalkPressed = true;
        }

    }

    private void FixedUpdate()
    {
        //check if player is on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Check update movement based on input
        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            vel.x = 0;

        }


        //talk
        if (isTalkPressed)
        {
            isTalkPressed = false;

            if (!isTalking)
            {
                isTalking = true;
            }
        }

        void ChangeAnimationState(string newAnimation)
        {
            if (currentAnimaton == newAnimation) return;

            animator.Play(newAnimation);
            currentAnimaton = newAnimation;
        }

    }
}

