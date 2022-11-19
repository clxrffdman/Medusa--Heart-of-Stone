using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemiesConverge : DialogueAction
{

    public ConvergeOnLocation convergeOnLocation;

    public override void PerformDialogueAction()
    {
        MakeEnemiesConvergeOnLocation();
    }

    private void MakeEnemiesConvergeOnLocation()
    {
        convergeOnLocation.MakeNpcsConvergeOnLocation();
    }
}