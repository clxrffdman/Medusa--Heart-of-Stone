using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheckTask : MonoBehaviour
{
    public bool defaultState = true;
    public ExpectedTask[] tasksToCheck;
    public bool newState;

    public GameObject[] objectsAffected;

    public void ChangeStateBasedOnTasksCompleted()
    {

        bool shouldActive = true;
        foreach (ExpectedTask et in tasksToCheck)
        {
            if (TaskManager.Instance.CheckTaskExists(et.taskID))
            {
                if (TaskManager.Instance.CheckTaskComplete(et.taskID) == et.expectedState)
                {

                }
                else
                {
                    shouldActive = false;
                }

            }
            else
            {
                if (et.expectedState == true)
                {
                    shouldActive = false;
                }
            }
        }

        if (shouldActive)
        {
            ChangeObjectStates(newState);
        }
        else
        {
            ChangeToDefaultState();
        }
    }

    public void ChangeObjectStates(bool state)
    {
        for (int i = 0; i < objectsAffected.Length; i++)
        {
            objectsAffected[i].SetActive(state);
        }
    }

    private void ChangeToDefaultState()
    {
        if (defaultState == true)
        {
            ChangeObjectStates(true);
        }
        else
        {
            ChangeObjectStates(false);
        }
    }
}
