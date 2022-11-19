using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ChargeLampHealth : EnemyHealth
{
    private PlayerController playerController;

    public float regenRate;
    public SpriteRenderer[] mainSprites;
    public bool notFull = true;
    public GameObject mask;
    public float fullMaskSize;
    public EnvironmentalObject eo;
    public Light2D mainLight;
    public float baseIntensity;
    public float maxIntensity;

    public EnemyController[] enemiesToActivate;

    public enum ObjectStates { Empty, Charged };

    // Start is called before the first frame update
    public override void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        currentHealth = baseHealth;
        ogMat = mainSprites[0].material;
        eo = GetComponent<EnvironmentalObject>();
        Invoke("UpdateFromSave", 0.05f);
    }

    void UpdateFromSave()
    {

        switch (eo.currentState)
        {
            case "Charged":
                currentHealth = 0;
                notFull = false;
                mask.transform.localScale = new Vector3(mask.transform.localScale.x, 0.175f * (((baseHealth - currentHealth) / baseHealth)), mask.transform.localScale.z);
                foreach (SpriteRenderer s in mainSprites)
                {
                    s.material = Resources.Load<Material>("Materials/laserbarShaderExtra");
                }
                mainLight.intensity = maxIntensity;
                hitTimer = -3f;
                break;
            case "Empty":
                break;
            default:
                break;
        }


    }

    // Update is called once per frame
    public override void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }

        if (hitTimer <= 0 && hitTimer > -2)
        {
            foreach (SpriteRenderer s in mainSprites)
            {
                s.material = ogMat;
            }
            hitTimer = -3f;

        }
    }

    public override void FixedUpdate()
    {
        if (notFull && currentHealth <= 0)
        {
            notFull = false;
            eo.SetCurrentState(ObjectStates.Charged.ToString());
            foreach (SpriteRenderer s in mainSprites)
            {
                s.material = Resources.Load<Material>("Materials/laserbarShaderExtra");
            }
            hitTimer = -3f;

            if (enemiesToActivate.Length != 0)
            {
                for (int i = 0; i < enemiesToActivate.Length; i++)
                {
                    EnemyController enemyController = enemiesToActivate[i].GetComponent<EnemyController>();
                    enemyController.SetAStarDestination(playerController.gameObject.transform);
                    enemyController.ActivateEnemy();
                }
            }


        }
        else if (notFull && currentHealth < baseHealth)
        {
            mask.transform.localScale = new Vector3(mask.transform.localScale.x, 0.1716972f * (((baseHealth - currentHealth) / baseHealth)), mask.transform.localScale.z);
            mainLight.intensity = (((baseHealth - currentHealth) / baseHealth) * maxIntensity);
            if (mainLight.intensity < baseIntensity)
            {
                mainLight.intensity = baseIntensity;
            }
            currentHealth += regenRate;
        }

        if (currentHealth > baseHealth)
        {
            currentHealth = baseHealth;
        }


    }

    public override void TakeDamage(float damage)
    {
        if (notFull)
        {
            hitTimer = 0.2f;
            currentHealth -= damage;
            foreach (SpriteRenderer s in mainSprites)
            {
                s.material = Resources.Load<Material>("Materials/laserbarShaderExtra");
            }
        }


    }
}
