using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{

    protected InventoryManager inventoryManager;
    protected SpriteRenderer itemPromptSpriteRenderer;
    protected Animator itemPromptAnimator;
    protected DialogueManager dialogueManager;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D[] boxColliders2D;
    protected LevelControl levelController;
    protected PickupObject pickupObject;
    [HideInInspector]
    public enum ObjectStates { Start, HasBeenCollected };

    public bool hasBeenCollected;
    public string itemName;
    public string itemKind;

    [Header ("Objects")]
    public GameObject itemPrompt;
    public GameObject inventoryFullSFX;
    public GameObject itemCollectSFX;

    public string taskCompleted;

    public DialogueSetup dialogueSetup;

    public override void Start()
    {
        base.Start();
        dialogueManager = FindObjectOfType<DialogueManager>();
        boxColliders2D = GetComponents<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        levelController = FindObjectOfType<LevelControl>();

        UpdateState();

        //itemPromptSpriteRenderer = itemPrompt.GetComponent<SpriteRenderer>();
        //itemPromptAnimator = itemPrompt.GetComponent<Animator>();

        pickupObject = GetComponent<PickupObject>();
        if (pickupObject != null)
        {
            itemName = pickupObject.itemName;
            pickupObject.currentState = ObjectStates.Start.ToString();
        }



        if (dialogueSetup != null)
        {
            dialogueSetup.gameObject.SetActive(false);
        }
    }

    public override void Update()
    {
        //ShowAndHideItemPrompt();
        DetermineClosestItemToPlayer();
        RemoveItemOnPlayerExit();
        CheckItemInteraction();
    }

    public void ShowAndHideItemPrompt()
    {
        if (canBeInteractedWith && playerController.close == gameObject)
        {
            itemPromptSpriteRenderer.enabled = true;
        }
        else if (playerController.close != gameObject || !canBeInteractedWith)
        {
            itemPromptSpriteRenderer.enabled = false;
        }
    }

    public void DetermineClosestItemToPlayer()
    {
        if (canBeInteractedWith && playerController.interactEnable == true)
        {
            if (playerController.close == null)
            {
                playerController.close = gameObject;
            }

            if (playerController.close != null && playerController.close != gameObject)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < Vector2.Distance(playerController.close.transform.position, player.transform.position))
                {
                    playerController.close = gameObject;
                }
            }
        }
    }

    public void RemoveItemOnPlayerExit()
    {
        if (!canBeInteractedWith && playerController.close == gameObject)
        {
            playerController.close = null;
        }
    }

    public virtual void CheckItemInteraction()
    {

        if (GameManager.Instance.pauseMenuActive || GameManager.Instance.inventoryActive || GameManager.Instance.dialogueActive)
        {
            return;
        }

        if (!hasBeenCollected && canBeInteractedWith && (Input.GetButtonDown("Fire1")) && playerController.interactEnable && playerController.close == gameObject)
        {

            if (inventoryManager.CheckInventoryFull())
            {
                PlayInventoryFullSFX();
            }
            else
            {
                inventoryManager.PickUp(itemKind);

                SetItemAsCollected();

                PlayItemCollectSFX();
                playerController.close = null;

                if (!string.IsNullOrEmpty(taskCompleted))
                {
                    SetTaskAsComplete();
                }

                if (dialogueSetup != null)
                {
                    dialogueSetup.gameObject.SetActive(true);
                    if(pickupObject != null)
                    {
                        dialogueManager.isInItemDialogue = true;
                    }
                    PlayDialogueImmediately();
                }
            }
        }
    }

    public override void UpdateState()
    {
        if(pickupObject == null) { return; }
        if (pickupObject.currentState == ObjectStates.Start.ToString())
        {
            hasBeenCollected = false;
        }

        if (pickupObject.currentState == ObjectStates.HasBeenCollected.ToString())
        {
            SetItemAsCollected();
        }
    }

    private void SetItemAsCollected()
    {
        hasBeenCollected = true;
        if (spriteRenderer != null) { spriteRenderer.enabled = false; }

        for (int i = 0; i < boxColliders2D.Length; i++)
        {
            boxColliders2D[i].enabled = false;
        }

        if (pickupObject != null)
        {
            pickupObject.currentState = ObjectStates.HasBeenCollected.ToString();
        }
    }

    public void SetItemAsUncollected()
    {
        hasBeenCollected = false;
        if (pickupObject != null)
        {
            pickupObject.currentState = ObjectStates.Start.ToString();
        }
    }

    public void PlayInventoryFullSFX()
    {
        Instantiate(inventoryFullSFX, transform.position, transform.rotation);
    }

    public void PlayItemCollectSFX()
    {
        Instantiate(itemCollectSFX, transform.position, transform.rotation);
    }

    public void PlayDialogueImmediately()
    {
        dialogueManager.ClearAllDialogueFromList();
        dialogueSetup.ConstructDialogue();
        dialogueManager.currentDialogueSetup = dialogueSetup;
        dialogueManager.ActivateDialogue();
    }

    public void SetTaskAsComplete()
    {
        TaskManager.Instance.SetTaskAsComplete(taskCompleted, true);
        levelController.UpdateStatesForEnvironmentalObjects();
        levelController.SetStatesForObjectCheckTasks();
    }

    public override void ResetStateToStart()
    {
        pickupObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
    }
}
