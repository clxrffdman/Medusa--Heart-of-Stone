using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string taskID;
    public bool completed;

    public Task(string id, bool completionState)
    {
        taskID = id;
        completed = completionState;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
