using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MinotaurLamp : EnemyHealth
{
    [Header("Sprite References")]
    public SpriteRenderer sr;
    public float regenRate;
    public SpriteRenderer[] mainSprites;
    public Sprite[] lampSpriteStates;
    public bool notFull = true;
    public GameObject mask;
    public float fullMaskSize;
    public EnvironmentalObject eo;
    [Header("Light2D stats")]
    public Light2D mainLight;
    public float baseIntensity;
    public float maxIntensity;

    [Header("Minotaur References")]
    public MinotaurController minotaurController;
    public MinotaurLampDetection detection;
    public EnemyHealth minotaurHealth;

    [Header("Misc")]
    public bool activatedByCharge;
    protected ActionController actionController;
    public SingleAction actionToActivate;

    public enum ObjectStates { Empty, Charged };

    // Start is called before the first frame update
    public override void Start()
    {
        actionController = FindObjectOfType<ActionController>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = lampSpriteStates[0];
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

            StartCoroutine(DamageMinotaur());


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
        if (activatedByCharge && notFull)
        {
            hitTimer = 0.2f;
            currentHealth -= damage;
            foreach (SpriteRenderer s in mainSprites)
            {
                s.material = Resources.Load<Material>("Materials/laserbarShaderExtra");
            }
        }


    }

    public void ActivateFromCharge()
    {
        sr.sprite = lampSpriteStates[1];
        actionController.DeactivateAllActions();
        actionToActivate.gameObject.SetActive(true);
    }

    public IEnumerator DamageMinotaur()
    {
        if (detection.isInRange && minotaurController.isCharging)
        {
            minotaurHealth.TakeDamage(1);
        }

        yield return new WaitForSeconds(1.5f);

        currentHealth = baseHealth;
        notFull = true;

        LeanTween.value(gameObject, mainLight.intensity, baseIntensity, 0.25f).setOnUpdate((float val) => {
            mainLight.intensity = val;
        });
        
        eo.SetCurrentState(ObjectStates.Empty.ToString());
        foreach (SpriteRenderer s in mainSprites)
        {
            s.material = ogMat;
        }
        hitTimer = 0.2f;
    }
}
