using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ActionController : MonoBehaviour
{
    private SingleAction[] actions;
    private SingleAction activeAction;
    private ActionSequence currentActionSequenceLoaded;

    [HideInInspector]
    public PlayerController playerController;
    [HideInInspector]
    public CinemachineVirtualCamera currentCamera;

    public ActionSequence[] actionSequences;
    
    void Awake()
    {
        actions = GetComponentsInChildren<SingleAction>();
        playerController = FindObjectOfType<PlayerController>();
        currentCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        actionSequences = GetComponentsInChildren<ActionSequence>();
        DeactivateAllActions();
        
    }

    private void Start()
    {
        Invoke("ActivateCorrectActionSequence", .01f);
        Invoke("ActivateFirstAction", .01f);
    }

    public void DeactivateAllActions()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
    }

    public void ActivateCorrectActionSequence()
    {
        foreach(ActionSequence actSeq in actionSequences)
        {
            bool isTrue = true;

            if(actSeq.taskToCheck.Length >= 0)
            {
                foreach (ExpectedTask expectedTask in actSeq.taskToCheck)
                {
                    if (expectedTask.taskID == "")
                    {

                    }
                    else if (TaskManager.Instance.CheckTaskExists(expectedTask.taskID))
                    {
                        if (TaskManager.Instance.CheckTaskComplete(expectedTask.taskID) != expectedTask.expectedState)
                        {
                            isTrue = false;
                        }
                    }
                    else
                    {
                        if (expectedTask.expectedState == true)
                        {
                            isTrue = false;
                        }
                    }
                }
            }
            
            if (isTrue)
            {
                if(currentActionSequenceLoaded == null)
                {
                    currentActionSequenceLoaded = actSeq;
                }
                else
                {
                    if (currentActionSequenceLoaded.priority < actSeq.priority)
                    {
                        currentActionSequenceLoaded = actSeq;
                    }
                }
            }
        }

        for (int i = 0; i < actionSequences.Length; i++)
        {
            if(actionSequences[i] != currentActionSequenceLoaded)
            {
                actionSequences[i].gameObject.SetActive(false);
            }
        }
    }

    public void ActivateFirstAction()
    {
        if (currentActionSequenceLoaded == null)
        {
            currentActionSequenceLoaded = actionSequences[0];
            
        }
        activeAction = currentActionSequenceLoaded.startingAction;
        activeAction.gameObject.SetActive(true);
    }

    public void ActivateSpecificAction(GameObject actionToActivate)
    {
        DeactivateAllActions();
        actionToActivate.SetActive(true);
    }

    public SingleAction GetActiveAction()
    {
        return activeAction;
    }
}


