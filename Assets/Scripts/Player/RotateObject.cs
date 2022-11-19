using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    protected bool isRotatingTowardsEnd = true;
    protected bool shouldMove;
    protected Transform currentTargetTransform;
    public Quaternion targetAngle;

    public Transform startTarget;
    public Transform endTarget;
    public float turnSpeed;
    public float pauseBeforeStartTime;
    public float pauseTime;

    public virtual void Start()
    {
        LookAtStartPoint();

        if(pauseBeforeStartTime != 0)
        {
            Invoke("EnableMovement", pauseBeforeStartTime);
        } else
        {
            EnableMovement();
        }

    }


    public virtual void Update()
    {
        if (shouldMove)
        {
            RotateTowardTarget();
            if (isRotatingTowardsEnd)
            {
                
                if (CheckIfDestinationReached())
                {
                    DisableMovement();
                    if(pauseTime > 0)
                    {
                        Invoke("ReverseRotation", pauseTime);
                    }
                    else
                    {
                        ReverseRotation();
                    }
                }
            } else
            {
                if (CheckIfDestinationReached())
                {
                    DisableMovement();
                    if (pauseTime > 0)
                    {
                        Invoke("ReverseRotation", pauseTime);
                    }
                    else
                    {
                        ReverseRotation();
                    }
                }
            }
        }
    }

    public bool CheckIfDestinationReached ()
    {
        if(Quaternion.Angle(transform.rotation, targetAngle) <= 0.01f)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void ReverseRotation()
    {
        ReverseDirection();
        EnableMovement();
    }

    public void ReverseDirection()
    {
        if (isRotatingTowardsEnd)
        {
            isRotatingTowardsEnd = false;
            SetRotationTarget(startTarget);
        } else
        {
            isRotatingTowardsEnd = true;
            SetRotationTarget(endTarget);
        }
    }

    public void RotateTowardTarget()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * turnSpeed);
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, currentTargetTransform.position - transform.position);
        desiredRotation = Quaternion.Euler(0, 0, desiredRotation.eulerAngles.z + 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnSpeed * Time.deltaTime);
    }

    public Quaternion SetRotationTarget(Transform target)
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        targetAngle = q;
        currentTargetTransform = target;
        return q;
    }

    public void LookAtStartPoint()
    {
        transform.rotation = SetRotationTarget(startTarget);
    }

    public void EnableMovement()
    {
        shouldMove = true;
    }

    public void DisableMovement()
    {
        shouldMove = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startTarget.transform.position, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endTarget.transform.position, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, startTarget.transform.position);
        Gizmos.DrawLine(transform.position, endTarget.transform.position);
    }
}
