using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBronzeStateSetter : StateSetter
{
    private MirrorBronzeInteractable mirrorBronzeInteractable;

    void Awake()
    {
        mirrorBronzeInteractable = FindObjectOfType<MirrorBronzeInteractable>();    
    }

    public override void SetCurrentState()
    {
        mirrorBronzeInteractable.UpdateState();
    }
}
