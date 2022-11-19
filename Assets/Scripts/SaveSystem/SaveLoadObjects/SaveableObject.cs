using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {Pickup, Enemy, Environmental }

public abstract class SaveableObject : MonoBehaviour
{
    protected string saveInfo;
    protected ObjectType objectType;

    private void Start()
    {
        SaveGameManager.Instance.SaveableObjects.Add(this);
    }

    public virtual void Save(int id)
    {
        SaveGameManager.Instance.savedObjects[id] = objectType + "_" + transform.position.ToString() + "_" + saveInfo;   
    }

    public virtual void Load(string[] values)
    {
        //transform.localPosition = SaveGameManager.Instance.StringToVector(values[1]);
    }

    public void DestroySaveable()
    {

    }

  
}
