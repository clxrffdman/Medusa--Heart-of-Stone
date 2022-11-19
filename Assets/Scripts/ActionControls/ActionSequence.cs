using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionSequence: MonoBehaviour
{
    public SingleAction startingAction;
    public ExpectedTask[] taskToCheck;
    public int priority;

    public ActionSequence()
    {

    }


   
}
