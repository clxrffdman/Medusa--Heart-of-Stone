using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    protected EnemyController enemyController;
    protected bool isInvunerable;
    protected AudioSource audioSource;

    public Material ogMat;
    public float hitTimer;
    public float invincibilityTime;
    public float baseHealth;
    public float currentHealth;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        enemyController = GetComponent<EnemyController>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = baseHealth;
        ogMat = enemyController.mainSprites[0].material;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }

        if(hitTimer <= 0 && hitTimer > -2)
        {
            foreach (SpriteRenderer s in enemyController.mainSprites)
            {
                s.material = ogMat;
            }
            hitTimer = -3f;
        }

        if (enemyController.isDead)
        {
            audioSource.Stop();
            return;
        }

        PlayHurtSfx();
        
    }

    public virtual void FixedUpdate()
    {
        if(currentHealth <= 0 && !enemyController.isDead)
        {
            enemyController.StartDeathCoroutine();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (!enemyController.isDead && !isInvunerable)
        {

            isInvunerable = true;
            Invoke("MakeEnemyVulnerable", invincibilityTime);
            hitTimer = invincibilityTime;
            currentHealth -= damage;
            
            if (currentHealth < 0)
            {
                currentHealth = 0f;
            }

            foreach (SpriteRenderer s in enemyController.mainSprites)
            {
                s.material = Resources.Load<Material>("Materials/stoned_shader");
            }

            if (enemyController.isShieldedSoldier && !enemyController.isBlock)
            {
                StartCoroutine(enemyController.BlockState());
            }
        }
        

    }

    private void MakeEnemyVulnerable()
    {
        isInvunerable = false;
    }

    private void PlayHurtSfx()
    {
        if (isInvunerable && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isInvunerable && audioSource.isPlaying)
        {
            Invoke("StopPlayingHurtSfx", 0.1f);
        }
    }

    private void StopPlayingHurtSfx()
    {
        if (isInvunerable) { return; }
        audioSource.Stop();
    }
}
