using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalObject : SaveableObject
{
    private Interactable interactableObject;
    public string itemName;
    public string currentState;

    private void Awake()
    {
        objectType = ObjectType.Environmental;
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
        currentState = values[3];
        if(interactableObject != null)
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
