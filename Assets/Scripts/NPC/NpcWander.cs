using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWander : MonoBehaviour
{
    private NpcController npcController;
    private DialogueManager dialogueManager;
    public DialogueSetup[] dialogueSetups;
    private int numberOfDestinations;
    private int nextDestinationIndex;
    private bool newDestinationSet;
    private float timeReachedDestination;
    private bool timerStarted;

    public float idleTime;
    public Transform[] targetDestinations;

    void Start()
    {
        npcController = GetComponentInChildren<NpcController>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        numberOfDestinations = targetDestinations.Length;
    }

    
    void Update()
    {

        if (npcController.hasReachedDestination && !timerStarted)
        {
            timeReachedDestination = Time.time;
            timerStarted = true;
        }

        if (ShouldMoveNpcToNextDestination())
        {
            SetNewDestination();
            npcController.ChangeStateToWandering();
        }
    }

    private bool ShouldMoveNpcToNextDestination()
    {
        bool npcShouldMoveToNextDestination = false;

        if(npcController.hasReachedDestination && (timeReachedDestination + idleTime < Time.time))
        {
            bool isCurrentDialogue = false;
            for (int i = 0; i < dialogueSetups.Length; i++)
            {
                if (dialogueManager.currentDialogueSetup == dialogueSetups[i])
                {
                    isCurrentDialogue = true;
                }
            }

            if (!isCurrentDialogue)
            {
                npcShouldMoveToNextDestination = true;
                timerStarted = false;
            }
        }

        return npcShouldMoveToNextDestination;
    }

    private void SetNewDestination()
    {
        int randomNumber = nextDestinationIndex;
        while(randomNumber == nextDestinationIndex)
        {
            randomNumber = Random.Range(0, numberOfDestinations);
        }

        nextDestinationIndex = randomNumber;
        npcController.targetDestination = targetDestinations[nextDestinationIndex];
        npcController.hasReachedDestination = false;
    }
}
