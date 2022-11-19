using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAction : MonoBehaviour
{

    protected ActionController actionController;
    protected SpriteRenderer spriteRenderer;
    protected bool canActivateScene;
    protected BoxCollider2D boxCollider;

    public bool playerInputActivated;
    public SingleAction actionToActivate;
    public GameObject portalSfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (playerInputActivated)
            {
                canActivateScene = true;
            }
            else
            {
                ActivateNextScene();
                PlayPortalSfx();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && playerInputActivated)
        {
            canActivateScene = false;
        }
    }

    void Start()
    {
        actionController = FindObjectOfType<ActionController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if(spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    void Update()
    {
        if(canActivateScene && Input.GetButtonDown("Fire1"))
        {
            ActivateNextScene();
            PlayPortalSfx();
        }    
    }

    protected virtual void ActivateNextScene()
    {
        actionController.DeactivateAllActions();
        actionToActivate.gameObject.SetActive(true);
        if (boxCollider) { boxCollider.enabled = false; }
    }

    private void PlayPortalSfx()
    {
        if(portalSfx != null)
        {
            Instantiate(portalSfx, transform.position, transform.rotation);
        }
    }
}
