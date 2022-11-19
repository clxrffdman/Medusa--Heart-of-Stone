using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverInstance : MonoBehaviour
{
    public bool isBehind;
    public GameObject player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHurtbox" && player.transform.position.y > transform.position.y)
        {
            isBehind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerHurtbox")
        {
            isBehind = false;
        }
    }

}
