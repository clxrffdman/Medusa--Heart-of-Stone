using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLift : EnemyController
{
    public Vector2 newPos;
    public float moveTime;
    public bool hasScreenShake;
    public CameraShake camShake;

    public override void Awake()
    {
        
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {


    }

    public override void ActivateEnemy()
    {
        print("ENEMY ACTIVATED");
        isSceneControlled = false;
        isIdle = false;
        isMove = false;
        StartCoroutine(Activate());
    }

    public override void SetAStarDestination(Transform target)
    {

    }

    public IEnumerator Activate()
    {
        LeanTween.moveLocal(gameObject, newPos, moveTime);

        yield return new WaitForSeconds(moveTime);

        if (hasScreenShake)
        {
            if(camShake != null)
            {
                float ogShake = camShake.shakeVariation;
                camShake.shakeVariation = 3f;
                camShake.shakeTimer = 0.5f;
                camShake.shakeVariation = ogShake;
            }
        }


    }



}
