using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvergeOnLocation : MonoBehaviour
{

    public NpcController[] npcControllers;
    public Transform[] targetLocations;

    public void MakeNpcsConvergeOnLocation()
    {
        for (int i = 0; i < npcControllers.Length; i++)
        {
            npcControllers[i].targetDestination = targetLocations[i];
            npcControllers[i].ChangeStateToInvestigating();
        }
    }
}
