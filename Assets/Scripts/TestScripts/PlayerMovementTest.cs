using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Base Player Stats")]
    public float moveSpeedX;
    public float moveSpeedY;

    [Header("Current Player Stats")]
    public bool isMoving;
    public Vector2 currentMoveSpeed;
    public float currentXSpeed;
    public float currentYSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.W))
        {
            currentYSpeed = moveSpeedY;
        }

        if (Input.GetKey(KeyCode.S))
        {
            currentYSpeed = -moveSpeedY;
        }

        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            currentYSpeed = 0.0f;
        }

        //

        if (Input.GetKey(KeyCode.D))
        {
            currentXSpeed = moveSpeedX;
        }

        if (Input.GetKey(KeyCode.A))
        {
            currentXSpeed = -moveSpeedX;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            currentXSpeed = 0.0f;
        }

        currentMoveSpeed = new Vector2(currentXSpeed, currentYSpeed);
        rb.velocity = currentMoveSpeed;

    }



    


}
