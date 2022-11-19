using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : SaveableObject
{
    private Interactable interactableObject;
    public string itemName;
    public string currentState;


    // Start is called before the first frame update
    private void Awake()
    {
        objectType = ObjectType.Pickup;
        interactableObject = GetComponent<Interactable>();
        itemName = gameObject.name;
    }

    public override void Save(int id)
    {
        saveInfo = itemName + "_" + currentState;
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        itemName = values[2];
        if(values[3] != "")
        {
            currentState = values[3];
        }

        if (interactableObject != null)
        {
            interactableObject.UpdateState();
        }
        base.Load(values);
    }

    public void SetCurrentState(string state)
    {
        currentState = state;
    }
}
