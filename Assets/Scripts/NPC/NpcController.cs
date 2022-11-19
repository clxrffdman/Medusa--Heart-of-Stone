using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NpcController : MonoBehaviour
{
    private StateMachine stateMachine;
    private IState idleNpc;
    private IState walkingNpc;
    private IState frozenNpc;
    private IState sceneControlledNpc;
    private IState wanderingNpc;
    private IState investigatingNpc;
    private PlayerController playerController;

    [HideInInspector]
    public AIPath aiPath;
    [HideInInspector]
    public AIDestinationSetter aIDestinationSetter;
    private Animator animator;
    private AudioSource audioSource;

    [Header("Settings")]
    public float baseSpeed;
    public float investigateSpeed;
    public float speed;
    public bool isFacingLeft { get; private set; }
    public Transform targetDestination;
    public bool hasReachedDestination = true;
    public bool shouldStopWanderingAfterInvestigating;

    [Header("Npc States")]
    public bool isIdle;
    public bool isWalking;
    public bool isFrozen;
    public bool isSceneControlled;
    public bool isWandering;
    public bool isInvestigating;

    [Header("Objects")]
    public Rigidbody2D npcRigidBody;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        npcRigidBody = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        playerController = FindObjectOfType<PlayerController>();

        stateMachine = gameObject.AddComponent<StateMachine>();
        idleNpc = new IdleNpc(this, animator);
        frozenNpc = new FrozenNpc(this, animator);
        walkingNpc = new WalkingNpc(this, animator);
        sceneControlledNpc = new SceneControlledNpc(this, animator);
        wanderingNpc = new WanderingNpc(this, animator);
        investigatingNpc = new InvestigatingNpc(this, animator);

        aiPath.maxSpeed = baseSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        StopAStarPathfinding();
    }

    // Update is called once per frame
    void Update()
    {
        this.stateMachine.ExecuteStateUpdate();
    }


    public void ChangeStateToIdle()
    {
        this.stateMachine.ChangeState(idleNpc);
    }

    public void ChangeStateToFrozen()
    {
        this.stateMachine.ChangeState(frozenNpc);
    }

    public void ChangeStateToWalking()
    {
        this.stateMachine.ChangeState(walkingNpc);
    }

    public void ChangeStateToSceneControlled()
    {
        this.stateMachine.ChangeState(sceneControlledNpc);
    }

    public void ChangeStateToWandering()
    {
        this.stateMachine.ChangeState(wanderingNpc);
    }

    public void ChangeStateToInvestigating()
    {
        this.stateMachine.ChangeState(investigatingNpc);
    }

    public void SetFacingDirection()
    {   
            if (aiPath.desiredVelocity.x >= 0.1f)
            {
                MakeNpcFaceRight();
            }
            else if (aiPath.desiredVelocity.x <= -0.1f)
            {
                MakeNpcFaceLeft();
            }
    }

    public void MakeNpcFaceLeft()
    {
        isFacingLeft = true;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -180f, transform.eulerAngles.z);
    }

    public void MakeNpcFaceRight()
    {
        isFacingLeft = false;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
    }

    public void MakeNpcFacePlayer()
    {
        Transform playerTransform = playerController.gameObject.transform;

        if(playerTransform.position.x > transform.position.x)
        {
            MakeNpcFaceRight();
        }
        else
        {
            MakeNpcFaceLeft();
        }
    }

    public void SetAStarDestination(Transform target)
    {
        targetDestination = target;
        aIDestinationSetter.targetASTAR = targetDestination;
    }

    public void StartAStarPathfinding()
    {
        aiPath.canSearch = true;
        aiPath.canMove = true;
    }

    public void StopAStarPathfinding()
    {
        aiPath.canSearch = false;
        aiPath.canMove = false;
    }

    public bool CheckIfEndReached()
    {
        if(targetDestination == null)
        {
            return true;
        }

        if(Vector2.Distance(transform.position, targetDestination.position) < 0.1f)
        {
            return true;
        } else
        {
            return false;
        }
        
    }


    public void PrintString(string stringToPrint)
    {
        print(stringToPrint);
    }
}
