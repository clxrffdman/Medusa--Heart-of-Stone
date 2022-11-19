using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public bool isFacingRight;

    public void FlipRight() {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, -180f, transform.eulerAngles.z);
        isFacingRight = true;

    }

    public void FlipLeft()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        isFacingRight = false;
    }

    public void MakeEnemyFaceWalkingDirection()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            FlipRight();
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            FlipLeft();
        }
    }
}
