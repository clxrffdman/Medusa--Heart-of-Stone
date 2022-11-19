using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakMirror : DialogueAction
{

    private MirrorInteractableScript mirror;

    void Start()
    {
        mirror = FindObjectOfType<MirrorInteractableScript>();
    }

    public override void PerformDialogueAction()
    {
        mirror.environmentalObject.currentState = (MirrorInteractableScript.ObjectStates.Broken.ToString());
        mirror.UpdateState();
    }
}
