using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used by Single Action to set a different state for a particular interactable.
public class StateSetter : MonoBehaviour
{
    public Interactable interactable;

    public virtual void SetCurrentState()
    {
        
    }
}
