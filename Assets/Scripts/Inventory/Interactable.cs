using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    protected PlayerController playerController;
    protected bool canBeInteractedWith;
    [HideInInspector]
    public GameObject player;


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canBeInteractedWith = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canBeInteractedWith = false;
        }
    }

    public virtual void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        playerController = player.GetComponent<PlayerController>();
    }

    public virtual void Update()
    {
        
    }

    public virtual void UpdateState()
    {

    }

    public virtual void ResetStateToStart()
    {

    }

}
