using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditActivator : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public GameObject credit;
    public string taskCompleted;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ActivateCredit();
            if (!System.String.IsNullOrEmpty(taskCompleted))
            {
                SetTaskComplete();
            }
        }
    }

    public void ActivateCredit()
    {
        credit.SetActive(true);
        boxCollider.enabled = false;
    }

    private void SetTaskComplete()
    {
        TaskManager.Instance.SetTaskAsComplete(taskCompleted, true);
    }
}
