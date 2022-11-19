using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartChest : DialogueAction
{
    private ChestInteractable chest;

    void Start()
    {
        chest = GetComponentInParent<ChestInteractable>();
    }

    public override void PerformDialogueAction()
    {
        chest.UpdateState();
    }
}
