using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerAI : MonoBehaviour
{

    public Transform target;
    public float speed = 2f;
    public float nextWaypointDistance = 3f;

    Path path;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        //// Check in a loop if we are close enough to the current waypoint to switch to the next one.
        //// We do this in a loop because many waypoints might be close to each other and we may reach
        //// several of them in the same frame.
        //reachedEndOfPath = false;
        //// The distance to the next waypoint in the path
        //float distanceToWaypoint;
        //while (true)
        //{
        //    // If you want maximum performance you can check the squared distance instead to get rid of a
        //    // square root calculation. But that is outside the scope of this tutorial.
        //    distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        //    if (distanceToWaypoint < nextWaypointDistance)
        //    {
        //        // Check if there is another waypoint or if we have reached the end of the path
        //        if (currentWaypoint + 1 < path.vectorPath.Count)
        //        {
        //            currentWaypoint++;
        //        }
        //        else
        //        {
        //            // Set a status variable to indicate that the agent has reached the end of the path.
        //            // You can use this to trigger some special code if your game requires that.
        //            reachedEndOfPath = true;
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}

        //// Slow down smoothly upon approaching the end of the path
        //// This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        ////var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        //// Direction to the next waypoint
        //// Normalize it so that it has a length of 1 world unit
        //Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
      
        //Vector3 velocity = dir * speed;

        //// If you are writing a 2D game you should remove the CharacterController code above and instead move the transform directly by uncommenting the next line
        //transform.position += velocity * Time.deltaTime;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            print("reachedEndOfPath");
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized;

        //Vector3 velocity = direction * speed;
        //transform.position += velocity * Time.deltaTime;

        Vector2 force = direction * speed * Time.deltaTime;
        rigidBody.AddForce(force);

        float distanceToWaypoint = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);

        if (distanceToWaypoint < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    IEnumerator UpdatePath()
    {
        seeker.StartPath(rigidBody.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f);
       
        //StartCoroutine(UpdatePath());
        
    }

    public void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }
}
