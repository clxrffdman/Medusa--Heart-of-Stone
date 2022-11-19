using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPatrol : MonoBehaviour
{
    private NpcController npcController;
    private int numberOfDestinations;
    private int nextDestinationIndex;
    private bool newDestinationSet;
    private float timeReachedDestination;
    private bool timerStarted;
    public bool canPatrol;
    [SerializeField] private Transform hunterTransform;
    public float idleTime;
    public Transform[] targetDestinations;

    void Start()
    {
        npcController = GetComponentInChildren<NpcController>();
        numberOfDestinations = targetDestinations.Length;
        canPatrol = true;
    }


    void Update()
    {

        if (!canPatrol)
        {
            return;
        }

        if (npcController.hasReachedDestination && !timerStarted)
        {
            timeReachedDestination = Time.time;
            timerStarted = true;
        }

        if (ShouldMoveNpcToNextDestination())
        {
            SetNewDestination();
            if (targetDestinations.Length == 1)
            {
                if (Vector3.Distance(hunterTransform.position, targetDestinations[0].position) > 0.1)
                {
                    npcController.ChangeStateToWandering();
                }
            }
            else
            {
                npcController.ChangeStateToWandering();
            }
        }
    }

    private bool ShouldMoveNpcToNextDestination()
    {
        bool npcShouldMoveToNextDestination = false;

        if (npcController.isInvestigating)
        {
            return npcShouldMoveToNextDestination;
        }

        if (npcController.hasReachedDestination && (timeReachedDestination + idleTime < Time.time))
        {
            npcShouldMoveToNextDestination = true;
            timerStarted = false;
        }

        return npcShouldMoveToNextDestination;
    }

    private void SetNewDestination()
    {
        if(targetDestinations.Length == 1)
        {
            npcController.targetDestination = targetDestinations[0];
            npcController.hasReachedDestination = false;
            return;
        }

        int randomNumber = nextDestinationIndex;
        while (randomNumber == nextDestinationIndex)
        {
            randomNumber = Random.Range(0, numberOfDestinations);
        }

        nextDestinationIndex = randomNumber;
        npcController.targetDestination = targetDestinations[nextDestinationIndex];
        npcController.hasReachedDestination = false;
    }

    public void StopPatrolling()
    {
        canPatrol = false;
    }
}
