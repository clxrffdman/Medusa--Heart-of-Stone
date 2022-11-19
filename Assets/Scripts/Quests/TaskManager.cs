using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public List<Task> taskList;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetTaskAsComplete(string taskID, bool state)
    {
        bool taskAlreadyExists = false;
        foreach (Task t in taskList)
        {
            if (t.taskID == taskID)
            {
                taskAlreadyExists = true;
                t.completed = state;
            }
        }

        if (!taskAlreadyExists)
        {
            taskList.Add(new Task(taskID, state));
        }
        
    }

    public bool CheckTaskComplete(string taskID)
    {
        bool rv = false;

        foreach(Task t in taskList)
        {
            if(t.taskID == taskID)
            {
                if(t.completed == true)
                {
                    rv = true;
                }
            }
        }

        return rv;
    }

    public bool CheckTaskExists(string taskID)
    {
        bool rv = false;

        foreach (Task t in taskList)
        {
            if (t.taskID == taskID)
            {
                rv = true;
            }
        }

        return rv;
    }

    public void LoadTasks(List<string> taskArray)
    {
        taskList.Clear();
        foreach (string s in taskArray)
        {
            bool state = false;
            string[] sSplit = s.Split('_');
            if(sSplit[1] == "True")
            {
                state = true;
            }
            taskList.Add(new Task(sSplit[0], state));
        }

    }

    public List<string> SaveTasks()
    {
        List<string> rv = new List<string>();
        

        foreach(Task t in taskList)
        {
            rv.Add(t.taskID + "_" + t.completed);
        }

        return rv;

    }
}
