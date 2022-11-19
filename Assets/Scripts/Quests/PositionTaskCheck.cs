using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTaskCheck : MonoBehaviour
{

    public TaskBasedPosition[] positions;
    // Start is called before the first frame update
    void Start()
    {
        TaskBasedPosition t = null;
        foreach(TaskBasedPosition tbp in positions)
        {
            if (TaskManager.Instance.CheckTaskComplete(tbp.taskToCheck))
            {
                if((t == null) || (tbp.priority > t.priority))
                {
                    t = tbp;
                }
            }
        }

        if(t != null)
        {
            transform.position = t.position;
        }


        //GameObject.Find("Player").transform.position = transform.position;
        FindObjectOfType<PlayerController>().gameObject.transform.position = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
