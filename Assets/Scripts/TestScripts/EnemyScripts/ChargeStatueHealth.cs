using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChargeStatueHealth : EnemyHealth
{
    public float regenRate;
    public SpriteRenderer[] mainSprites;
    public bool notFull = true;
    public GameObject mask;
    public float fullMaskSize;
    public EnvironmentalObject eo;
    

    public enum ObjectStates { Empty, Charged};

    // Start is called before the first frame update
    public override void Start()
    {
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
                mask.transform.localScale = new Vector3(mask.transform.localScale.x, 0.1716972f * (((baseHealth - currentHealth) / baseHealth)), mask.transform.localScale.z);
                foreach (SpriteRenderer s in mainSprites)
                {
                    s.material = Resources.Load<Material>("Materials/laserbarShaderExtra");
                }
                

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

        }
        else if(notFull && currentHealth < baseHealth)
        {
            mask.transform.localScale = new Vector3(mask.transform.localScale.x, 0.1716972f * (((baseHealth - currentHealth) / baseHealth)), mask.transform.localScale.z);
            
            currentHealth += regenRate;
        }

        if(currentHealth > baseHealth)
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
