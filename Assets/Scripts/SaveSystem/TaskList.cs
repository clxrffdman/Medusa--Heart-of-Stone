using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    public bool setTaskListOnLoad;
    public List<Task> tasksList;
    
    public void SetTaskManagerTasksToTaskList()
    {
        TaskManager.Instance.taskList.Clear();
        TaskManager.Instance.taskList = tasksList;
    }

    public void SetAllTasksAsComplete()
    {
        TaskManager.Instance.taskList.Clear();
        TaskManager.Instance.taskList = tasksList;
        for (int i = 0; i < TaskManager.Instance.taskList.Count; i++)
        {
            TaskManager.Instance.taskList[i].completed = true;
        }
    }

    public void ResetAllTasks()
    {
        TaskManager.Instance.taskList.Clear();
    }

    public void SetTasksListOnLoad()
    {
        if (setTaskListOnLoad)
        {
            tasksList.Clear();
            tasksList = TaskManager.Instance.taskList;
        }
    }
}
