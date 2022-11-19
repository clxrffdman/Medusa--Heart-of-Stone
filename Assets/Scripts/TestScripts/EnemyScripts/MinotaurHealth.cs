using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurHealth : EnemyHealth
{
    public MinotaurController minotaurController;


    public override void Start()
    {
        base.Start();

    }

    public override void TakeDamage(float damage)
    {
        if (!enemyController.isDead)
        {
            hitTimer = 0.2f;
            currentHealth -= damage;
            if(currentHealth > 0)
            {
                minotaurController.StartCoroutine(minotaurController.StunnedState());
            }


   
            foreach (SpriteRenderer s in enemyController.mainSprites)
            {
                s.material = Resources.Load<Material>("Materials/stoned_shader");
            }
        }


    }
}
