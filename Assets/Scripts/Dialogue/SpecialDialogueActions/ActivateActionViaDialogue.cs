using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateActionViaDialogue : DialogueAction
{
    private ActionController actionController;
    public GameObject actionToActivate;


    void Start()
    {
        actionController = FindObjectOfType<ActionController>();
    }

    public override void PerformDialogueAction()
    {
        ActivateSpecificAction();
    }

    private void ActivateSpecificAction()
    {
        actionController.ActivateSpecificAction(actionToActivate);
    }
}