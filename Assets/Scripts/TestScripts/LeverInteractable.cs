using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : Interactable
{
    public EnvironmentalObject environmentalObject;
    public bool isChimeraBabiesCage;
    public string associatedTask;
    public SpriteRenderer chestSpriteRenderer;
    public Sprite[] spriteStates;
    public SpriteRenderer cageRenderer;
    public Sprite[] cageStates;
    public Vector3 newCagePos;
    public GameObject startDialogueSetup;
    public GameObject activatedDialogueSetup;
    public GameObject actionActivator;

    public enum ObjectStates { Start, Activated};

    // Start is called before the first frame update
    public override void Start()
    {
        chestSpriteRenderer = GetComponent<SpriteRenderer>();
        environmentalObject = GetComponent<EnvironmentalObject>();
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
        base.Start();
    }

    public override void UpdateState()
    {
        if (TaskManager.Instance.CheckTaskComplete(associatedTask))
        {
            environmentalObject.SetCurrentState(ObjectStates.Activated.ToString());
        }

        
        if (TaskManager.Instance.CheckTaskComplete(associatedTask) && !TaskManager.Instance.CheckTaskComplete(associatedTask))
        {
            actionActivator.SetActive(true);
        }
        else
        {
            actionActivator.SetActive(false);
        }
        

        switch (environmentalObject.currentState)
        {
            case "Start":
                chestSpriteRenderer.sprite = spriteStates[0];
                if (isChimeraBabiesCage)
                {
                    cageRenderer.sprite = cageStates[0];
                }

                startDialogueSetup.SetActive(true);
                activatedDialogueSetup.SetActive(false);
                break;
            case "Activated":
                chestSpriteRenderer.sprite = spriteStates[1];
                if (isChimeraBabiesCage)
                {
                    cageRenderer.sprite = cageStates[1];
                    cageRenderer.gameObject.transform.position = newCagePos;
                }

                startDialogueSetup.SetActive(false);
                activatedDialogueSetup.SetActive(true);
                break;
            default:
                break;
        }
    }

    public override void ResetStateToStart()
    {
        environmentalObject.SetCurrentState(ObjectStates.Start.ToString());
        UpdateState();
    }

}
