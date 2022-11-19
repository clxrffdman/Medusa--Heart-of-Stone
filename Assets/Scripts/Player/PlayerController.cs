using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerController : MonoBehaviour
{
    private StateMachine stateMachine;
    private IState idlePlayer;
    private IState walkingPlayer;
    private IState talkingPlayer;
    private IState frozenPlayer;
    private IState sceneControlledPlayer;
    private IState hurtPlayer;
    private IState deadPlayer;
    private AIPath aiPath;
    private AIDestinationSetter aIDestinationSetter;
    private Animator animator;
    private AudioSource audioSource;
    

    [Header("Settings")]
    public float baseSpeed;
    public float attackSpeed;
    public float laserSpeed;
    public float speed;
    public float minInputNumber;
    public float medusaLaserWalkSpeedMultiplier;
    public bool isFacingLeft { get; private set; }
    public Transform targetDestination;
    public bool hasReachedDestination = true;

    [Header("Player States")]
    public bool isIdle;
    public bool isWalking;
    public bool isTalking;
    public bool isFrozen;
    public bool isSceneControlled;
    public bool isLaserControlled;
    public bool isDead;
    public bool laserEnabled;
    public bool interactEnable = true;

    [Header("Objects")]
    public Rigidbody2D playerRigidBody;
    public GameObject laserEndPoint;
    public GameObject hecateMask;
    public GameObject damageSfx;

    [Header("Saved Stats")]
    public int currentSceneIndex;
    public Vector3 playerPos;
    public int playerHealth;
    public GameObject close;

    void Awake()
    {
        //print("awaking");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();

        stateMachine = gameObject.AddComponent<StateMachine>();
        idlePlayer = new IdlePlayer(this, animator);
        walkingPlayer = new WalkingPlayer(this, animator);
        frozenPlayer = new FrozenPlayer(this, animator);
        sceneControlledPlayer = new SceneControlledPlayer(this, animator);
        hurtPlayer = new HurtPlayer(this, animator);
        deadPlayer = new DeadPlayer(this, animator);

        currentSceneIndex = gameObject.scene.buildIndex;
        speed = baseSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        StopAStarPathfinding();
        DeactivateMask();
    }

    private void FixedUpdate()
    {
        playerPos = transform.position;
    }

    void Update()
    {
        this.stateMachine.ExecuteStateUpdate();
    }


    public void MovePlayer ()
    {
        if (GameManager.Instance.isPaused){ return; }

        playerRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        
    }

    public bool CheckForPlayerMoveInput()
    {
        if (GameManager.Instance.isPaused)
        {
            return false;
        }

        if((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > minInputNumber || Mathf.Abs(Input.GetAxisRaw("Vertical")) > minInputNumber))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void SetFacingDirection()
    {
        if (GameManager.Instance.isPaused) { return; }

        if (!isSceneControlled)
        {
            if (!isLaserControlled)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    MakePlayerFaceLeft();
                }

                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    MakePlayerFaceRight();
                }
            } else
            {
                if (laserEndPoint.transform.position.x < transform.position.x)
                {
                    MakePlayerFaceLeft();
                } else if (laserEndPoint.transform.position.x > transform.position.x)
                {
                    MakePlayerFaceRight();
                }
            }
        }
        else
        {
            if(aiPath.desiredVelocity.x >= 0.1f)
            {
                MakePlayerFaceRight();
            } else if (aiPath.desiredVelocity.x <= -0.1f)
            {
                MakePlayerFaceLeft();
            }
        }
        
    }

    public void MakePlayerFaceLeft()
    {
        isFacingLeft = true;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -180f, transform.eulerAngles.z);
    }

    public void MakePlayerFaceRight()
    {
        isFacingLeft = false;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
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
        hasReachedDestination = false;
    }

    public void StopAStarPathfinding()
    {
        aiPath.canSearch = false;
        aiPath.canMove = false;
        hasReachedDestination = true;
        targetDestination = null;
        aIDestinationSetter.targetASTAR = null;
    }

    public bool CheckIfEndReached()
    {
        if (targetDestination == null)
        {
            return true;
        }

        if (Vector2.Distance(transform.position, targetDestination.position) < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActivateMask()
    {
        hecateMask.SetActive(true);
    }

    public void DeactivateMask()
    {
        hecateMask.SetActive(false);
    }

    public void PlayHurtSfx()
    {
        Instantiate(damageSfx, transform.position, transform.rotation);
    }

    public void PrintString(string stringToPrint)
    {
        print(stringToPrint);
    }

    public void ChangeStateToIdle()
    {
        this.stateMachine.ChangeState(idlePlayer);
    }

    public void ChangeStateToFrozen()
    {
        this.stateMachine.ChangeState(frozenPlayer);
    }

    public void ChangeStateToWalking()
    {
        this.stateMachine.ChangeState(walkingPlayer);
    }

    public void ChangeStateToTalking()
    {
        this.stateMachine.ChangeState(talkingPlayer);
    }

    public void ChangeStateToSceneControlled()
    {
        this.stateMachine.ChangeState(sceneControlledPlayer);
    }

    public void ChangeStateToHurt()
    {
        this.stateMachine.ChangeState(hurtPlayer);
    }

    public void ChangeStateToDead()
    {
        this.stateMachine.ChangeState(deadPlayer);
    }
}
