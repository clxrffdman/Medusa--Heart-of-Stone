using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : EnemyController
{
    public Rigidbody2D rb;

    [Header("Charge Stats")]
    public MinotaurChargeShield chargeBox;
    public PolygonCollider2D chargeHitbox;
    public float chargeWindup;
    public bool isCharging;
    public float maxStunDuration;
    public float chargeSpeed;
    [Header("Rock Stats")]
    public GameObject fallingRock;
    public float rockSpawnRange;
    public int rockCount;
    [Header("MISC")]
    public MinotaurLamp minotaurLamp;
    protected ActionController actionController;
    public SingleAction actionToActivate;
    public SingleAction deathActionToActivate;
    public CameraShake camShake;


    // Start is called before the first frame update
    public override void Start()
    {
        actionController = FindObjectOfType<ActionController>();
        currentTargetSpeed = baseSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();
        
    }

   

    // Update is called once per frame

    public override IEnumerator Death()
    {
        StopAllCoroutines();
        isMove = false;
        chargeHitbox.enabled = false;
        rb.velocity = new Vector2(0, 0);
        mainAnim.SetBool("isWalk", false);
        mainAnim.SetBool("isBlock", false);
        mainAnim.SetBool("isAttack", false);

        if (GameObject.Find("LevelManager") != null)
        {

            //GameObject.Find("LevelManager").GetComponent<LevelControl>().enemiesSlain += 1;
        }

        actionController.DeactivateAllActions();
        deathActionToActivate.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
    }

    public IEnumerator StunnedState()
    {
        StopCoroutine(BlockState());

        rb.velocity = new Vector2(0, 0);
        chargeHitbox.enabled = false;
        isMove = false;
        mainAnim.SetBool("isBlock", false);
        mainAnim.SetBool("isAttack", false);

        yield return new WaitForSeconds(maxStunDuration);

        aiPath.enabled = true;
        isBlock = false;
        
        mainAnim.SetBool("isBlock", false);
        attackAnimation = false;
        isMove = true;

        if(GetComponent<MinotaurHealth>().currentHealth == GetComponent<MinotaurHealth>().baseHealth - 1)
        {
            actionController.DeactivateAllActions();
            actionToActivate.gameObject.SetActive(true);
        }

    }

    public override IEnumerator BlockState()
    {
        isMove = false;
        attackAnimation = true;
        mainAnim.SetBool("isBlock", true);
        print("ENEMY BLOCK");

        
        aiPath.enabled = false;

        yield return new WaitForSeconds(chargeWindup);

        if (GetComponentInChildren<EnemyGFX>().isFacingRight)
        {
            rb.velocity = new Vector2(chargeSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-chargeSpeed, 0);
        }
        isCharging = true;
        chargeHitbox.enabled = true;

        while (!chargeBox.hitWall)
        {
            yield return new WaitForSeconds(0.75f);
            if (validY)
            {
                chargeHitbox.enabled = true;
            }
            else
            {
                chargeHitbox.enabled = false;

            }
        }

        chargeHitbox.enabled = false;

        isCharging = false;
        if (!minotaurLamp.activatedByCharge)
        {
            minotaurLamp.activatedByCharge = true;
            minotaurLamp.ActivateFromCharge();


        }
        else
        {
            for (int i = 0; i < rockCount; i++)
            {
                if (GetComponentInChildren<EnemyGFX>().isFacingRight)
                {
                    var fr = Instantiate(fallingRock, transform.position + new Vector3(Random.Range(-rockSpawnRange, 0), 9f + Random.Range(0, 3), 0), Quaternion.identity);
                    float randomScale = Random.Range(-0.25f, 0.25f);
                    fr.transform.localScale += new Vector3(randomScale, randomScale, 0);
                }
                else
                {
                    var fr = Instantiate(fallingRock, transform.position + new Vector3(Random.Range(0, rockSpawnRange), 9f + Random.Range(0, 3), 0), Quaternion.identity);
                    float randomScale = Random.Range(-0.25f, 0.75f);
                    fr.transform.localScale += new Vector3(randomScale, randomScale, 0);
                }

                yield return new WaitForSeconds(0.15f);

            }
        }

        chargeHitbox.enabled = false;

        float ogShake = camShake.shakeVariation;
        camShake.shakeVariation = 3f;
        camShake.shakeTimer = 2.5f;

        

        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(Random.Range(blockMinDuration, blockMaxDuration));

        aiPath.enabled = true;

        isBlock = false;
        
        mainAnim.SetBool("isBlock", false);

        yield return new WaitForSeconds(0.6f);
        camShake.shakeVariation = ogShake;
        attackAnimation = false;
        isMove = true;


    }
}
