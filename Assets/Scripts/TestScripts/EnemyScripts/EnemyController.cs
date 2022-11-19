using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;

public class EnemyController : MonoBehaviour
{

    
    protected AIDestinationSetter aIDestinationSetter;
    protected Animator mainAnim;
    protected Animator shieldArmAnim;
    protected Animator spearArmAnim;
    protected PlayerController playerController;
    protected Rigidbody2D rigidBody;
    protected RVOController rvoController;
    protected bool canAttack;
    protected EnemyGFX enemyGFX;

    public bool isShieldedSoldier;
    public bool attackAnimation;
    public bool blockAnimation;
    protected float distanceToPlayer;

    [Header("Structures")]
    public GameObject player;

    [Header("Enemy Stats: Detection (block > attack)")]
    public float attackDetectRange;
    public float blockDetectRange;
    public float turnAroundRange;
    [Header("Enemy Stats: Cooldowns")]
    public float baseAttackCooldown;
    public float baseBlockCooldown;
    [Header("Enemy Stats: State Duration")]
    public float attackDuration;
    public float blockMinDuration;
    public float blockMaxDuration;
    public float pauseBeforeMovingAgain;
    [Header("Enemy Stats: Attack Values")]
    public float attackDamage;
    public float attackWindupTime;
    public float attackRange;
    public float attackMoveDuration;
    public float randomizeFactor;

    [Header("Enemy Stats: Movement Speed")]
    public float baseSpeed;
    
    [Header("Current Enemy State")]
    public float currentAttackCooldown;
    public float currentBlockCooldown;
    public float currentTargetSpeed;
    public bool inAttackRange;
    public bool inBlockRange;
    public bool validY;
    public bool isSceneControlled;
    public bool isIdle;
    public bool isMove;
    public bool isAttack;
    public bool isBlock;
    public bool isDead;

    [Header("Miscellaneous")]
    public AIPath aiPath;
    public Transform targetDestination;
    public bool hasReachedDestination = true;
    public LayerMask playerMask;
    public SpriteRenderer[] mainSprites;
    public PolygonCollider2D damageCollider;
    public float checkDistance;
    public float baseAttackRange;
    public float yAttackRange;
    public bool playerIsLeft;
    public BoxCollider2D boxColliderBody;
    public GameObject attackSfx;
    public GameObject deathSfx;

    // Start is called before the first frame update

    public virtual void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        playerController = player.GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        rvoController = GetComponent<RVOController>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        mainAnim = transform.GetChild(0).GetComponent<Animator>();
        spearArmAnim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        shieldArmAnim = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        mainSprites = GetComponentsInChildren<SpriteRenderer>();
        enemyGFX = GetComponentInChildren<EnemyGFX>();
    }

    public virtual void Start()
    {
        RandomizeStats();
        currentTargetSpeed = baseSpeed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isSceneControlled)
        {
            if (!hasReachedDestination)
            {
                isMove = true;
                if (CheckIfEndReached())
                {
                    StopAStarPathfinding();
                    isMove = false;
                }
            }
        }
        else
        {
            if (!isIdle && !playerController.isDead)
            {
                if (!isDead)
                {
                    if (currentAttackCooldown >= 0 && !attackAnimation)
                    {
                        currentAttackCooldown -= Time.deltaTime;
                    }

                    //if (currentBlockCooldown >= 0 && !attackAnimation)
                    //{
                    //    currentBlockCooldown -= Time.deltaTime;
                    //}

                    if (!attackAnimation)
                    {
                        //if (isBlock && isShieldedSoldier)
                        //{
                        //    StartCoroutine(BlockState());
                        //}
                        if (isAttack && !isBlock)
                        {
                            StartCoroutine(AttackState());
                        }
                        CheckRange();
                    }

                    if (canAttack)
                    {
                        if(isBlock || isAttack)
                        {
                            PauseAStarPathfinding();
                        }
                        else if (!isDead)
                        {
                            RestartAStarPathfinding();
                        }
                    }
                }
            }
        }
    }

    public virtual void FixedUpdate()
    {
        CheckValidY();

        UpdateMoveAnim();

        if(!isAttack || !isBlock)
        {
            enemyGFX.MakeEnemyFaceWalkingDirection();
        }
        
    }

    public virtual void ActivateEnemy()
    {
        isSceneControlled = false;
        isIdle = false;
        isMove = true;
        aiPath.canSearch = true;
        aiPath.canMove = true;
        canAttack = true;
    }

    public void DeactivateEnemy()
    {
        isSceneControlled = true;
        isIdle = true;
        isMove = false;
        aiPath.canSearch = false;
        aiPath.canMove = false;
        canAttack = false;
    }

    void CheckValidY()
    {
        validY = false;

        if (Physics2D.Raycast(transform.position, Vector3.right, checkDistance, playerMask))
        {
            validY = true;
        }

        if (Physics2D.Raycast(transform.position, -Vector3.right, checkDistance, playerMask))
        {
            validY = true;
        }
    }

    void UpdateMoveAnim()
    {
        if (isMove)
        {
            mainAnim.SetBool("isWalk", true);
            if (aiPath.maxSpeed == 0)
            {
                LeanTween.value(gameObject, 0f, currentTargetSpeed, 0.2f).setOnUpdate((float val) => {
                    aiPath.maxSpeed = val;
                });
            }
        }
        else
        {
            mainAnim.SetBool("isWalk", false);
            aiPath.maxSpeed = 0;
        }
    }

    public virtual void SetAStarDestination(Transform target)
    {
        targetDestination = target;
        aIDestinationSetter.targetASTAR = targetDestination;
    }

    public virtual void StartAStarPathfinding()
    {
        aiPath.canSearch = true;
        aiPath.canMove = true;
        hasReachedDestination = false;
    }

    public virtual void StopAStarPathfinding()
    {
        aiPath.canSearch = false;
        aiPath.canMove = false;
        hasReachedDestination = true;
        targetDestination = null;
    }

    public virtual void PauseAStarPathfinding()
    {
        aiPath.canSearch = false;
        aiPath.canMove = false;
    }

    public virtual void RestartAStarPathfinding()
    {
        aiPath.canSearch = true;
        aiPath.canMove = true;
    }

    public virtual bool CheckIfEndReached()
    {
        return aiPath.reachedEndOfPath;
    }

    public void CheckRange()
    {
        if(player.transform.position.x < transform.position.x)
        {
            playerIsLeft = true;
        }
        else
        {
            playerIsLeft = false;
        }

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //if (distanceToPlayer < blockDetectRange && distanceToPlayer > attackDetectRange)
        //{
        //    inBlockRange = true;
        //}
        //else
        //{
        //    inBlockRange = false;
        //}

        if (distanceToPlayer < attackDetectRange)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }


        //if (inBlockRange && validY && currentBlockCooldown <= 0)
        //{
        //    isBlock = true;
        //    currentBlockCooldown = baseBlockCooldown;
            
        //}

        if (inAttackRange && validY && currentAttackCooldown <= 0)
        {
            isAttack = true;
            currentAttackCooldown = baseAttackCooldown;
            if(currentBlockCooldown < baseBlockCooldown * (3/4))
            {
                currentBlockCooldown = baseBlockCooldown * (3/4);
            }
        }

        if (inAttackRange && !validY && currentAttackCooldown <= 0)
        {
            aiPath.endReachedDistance = yAttackRange;
            
        }
        else
        {
            aiPath.endReachedDistance = baseAttackRange;
        }

    }

    void RandomizeStats()
    {
        baseSpeed = baseSpeed + Random.Range(-randomizeFactor, randomizeFactor);
    }

    private float RandomBaseSpeed()
    {
        float newBaseSpeed = baseSpeed;

        if(currentTargetSpeed > baseSpeed)
        {
            newBaseSpeed = baseSpeed - randomizeFactor;
        }

        if(currentTargetSpeed <= baseSpeed)
        {
            newBaseSpeed = baseSpeed + randomizeFactor;
        }

        if(currentTargetSpeed == baseSpeed)
        {
            newBaseSpeed = baseSpeed + Random.Range(-randomizeFactor, randomizeFactor);
        }
        
        return newBaseSpeed;
    }

    public virtual IEnumerator AttackState()
    {
        if(distanceToPlayer < turnAroundRange)
        {
            if (playerIsLeft)
            {
                enemyGFX.FlipLeft();
            }
            else
            {
                enemyGFX.FlipRight();
            }
        }

        isMove = false;
        attackAnimation = true;
        mainAnim.SetBool("isAttack", true);

        yield return new WaitForSeconds(attackWindupTime);

        PlayAttackSfx();
        if (enemyGFX.isFacingRight)
        {
            LeanTween.value(gameObject, transform.position.x, transform.position.x + attackRange, attackMoveDuration).setOnUpdate((float val) =>
            {
                transform.position = new Vector2(val, transform.position.y);
            });
        }
        else
        {
            LeanTween.value(gameObject, transform.position.x, transform.position.x - attackRange, attackMoveDuration).setOnUpdate((float val) =>
            {
                transform.position = new Vector2(val, transform.position.y);
            });
        }

        yield return new WaitForSeconds(attackDuration);

        
        currentTargetSpeed = RandomBaseSpeed();
        mainAnim.SetBool("isAttack", false);
        

        yield return new WaitForSeconds(pauseBeforeMovingAgain);

        isAttack = false;
        isMove = true;
        attackAnimation = false;
    }

    public virtual IEnumerator BlockState()
    {
        isMove = false;
        attackAnimation = true;
        mainAnim.SetBool("isBlock", true);
        isBlock = true;
        
        yield return new WaitForSeconds(Random.Range(blockMinDuration, blockMaxDuration));
        
        currentTargetSpeed = RandomBaseSpeed();
        mainAnim.SetBool("isBlock", false);
        
        yield return new WaitForSeconds(pauseBeforeMovingAgain);

        isBlock = false;
        isMove = true;
        attackAnimation = false;
    }

    public virtual IEnumerator Death()
    {
        StopAllCoroutines();
        StopAStarPathfinding();
        rvoController.radius = 0f;
        isMove = false;
        isDead = true;
        aiPath.maxSpeed = 0;

        mainAnim.SetBool("isWalk", false);
        mainAnim.SetBool("isDead", true);

        if (boxColliderBody != null)
        {
            boxColliderBody.enabled = false;
        }

        PlayDeathSfx();

        yield return new WaitForSeconds(1f);
    }

    public void StartDeathCoroutine()
    {
        StartCoroutine(Death());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector3(transform.position.x - checkDistance, transform.position.y, transform.position.z), new Vector3(transform.position.x + checkDistance, transform.position.y, transform.position.z));
        
        
    }

    private void PlayDeathSfx()
    {
        Instantiate(deathSfx, transform.position, transform.rotation);
    }

    private void PlayAttackSfx()
    {
        Instantiate(attackSfx, transform.position, transform.rotation);
    }
}
