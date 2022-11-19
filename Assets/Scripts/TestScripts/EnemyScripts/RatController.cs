using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RatController : EnemyController
{

    [Header("RandomPathing")]
    public GameObject[] potentialLocations;
    public int currentTarget;

    public override void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        aiPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        mainAnim = transform.GetChild(0).GetComponent<Animator>();
        mainSprites = GetComponentsInChildren<SpriteRenderer>();
    }


    public override void Start()
    {
        currentTargetSpeed = baseSpeed;
    }

    public override void Update()
    {

        if (!isIdle)
        {
            if (!isDead)
            {
                isMove = true;
                if (CheckIfEndReached())
                {

                    currentTarget += 1;
                    if (currentTarget > potentialLocations.Length - 1)
                    {
                        currentTarget = 0;
                    }

                    SetAStarDestination(potentialLocations[currentTarget].transform);


                }
            }
            else
            {
                isMove = false;
            }
        }

    }


    public override void ActivateEnemy()
    {
        SetAStarDestination(potentialLocations[currentTarget].transform);
        print("ENEMY ACTIVATED");
        isSceneControlled = false;
        isIdle = false;
        isMove = true;
        aiPath.canSearch = true;
        aiPath.canMove = true;
    }
}
