using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierVisionCheck : MonoBehaviour
{

    private PlayerController playerController;
    public bool visible;
    public SpotlightController sc;
    //public NpcController npcController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        sc = Resources.FindObjectsOfTypeAll<SpotlightController>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(visible && sc != null && sc.isVisible)
        {
            sc.isSpotted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHurtbox")
        {
            visible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHurtbox")
        {
            visible = false;
        }
    }

}
