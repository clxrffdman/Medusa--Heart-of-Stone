using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatingGameController : MonoBehaviour
{
    private ActionController actionController;
    private DialogueManager dialogueManager;
    private FadeController fadeController;
    private LevelControl levelController;
    private PlayerController playerController;
    private string currentSuitor;
    private DialogueSetup currentDialogueSetup;

    public float fadeTime;
    public GameObject datingGameBackground;
    public GameObject darkBackground;
    public GameObject hephaestusDialogueMini;
    public GameObject hephaestusFinishedAction;
    public GameObject dionysusDialogueMini;
    public GameObject dionysusFinishedAction;
    public GameObject sphinxDialogueMini;
    public GameObject sphinxFinishedAction;
    public DialogueSetup hephaestusDialogueSetup;
    public DialogueSetup dionysusDialogueSetup;
    public DialogueSetup sphinxDialogueSetup;
    
    

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        fadeController = GetComponent<FadeController>();
        levelController = FindObjectOfType<LevelControl>();
        playerController = FindObjectOfType<PlayerController>();
        actionController = FindObjectOfType<ActionController>();
        
    }

    public void StartDatingGame(string suitorName)
    {
        FadeOut();
        levelController.inDatingGame = true;
        Invoke("ActivateDatingGameElements", fadeTime);
        SetCurrentDialogueSetup(suitorName);
        DeactivateDialogueMini();
    }

    public void ActivateDatingGameElements()
    {
        datingGameBackground.SetActive(true);
        darkBackground.SetActive(false);
        FadeIn();
        print("fading in from activating dating");
        Invoke("ActivateDatingGameDialogue", fadeTime);
    }

    public void ActivateDatingGameDialogue()
    {
        PlayDialogueImmediately();
    }

    public void PlayDialogueImmediately()
    {
        dialogueManager.ClearAllDialogueFromList();
        currentDialogueSetup.ConstructDialogue();
        dialogueManager.currentDialogueSetup = currentDialogueSetup;
        dialogueManager.ActivateDialogue();
    }

    public void SetCurrentDialogueSetup(string suitorName)
    {
        switch (suitorName)
        {
            case "Hephaestus":
                currentDialogueSetup = hephaestusDialogueSetup;
                currentSuitor = "Hephaestus";
                break;
            case "Dionysus":
                currentDialogueSetup = dionysusDialogueSetup;
                currentSuitor = "Dionysus";
                break;
            case "Sphinx":
                currentDialogueSetup = sphinxDialogueSetup;
                currentSuitor = "Sphinx";
                break;
            default:
                break;
        }
    }

    public void DeactivateDialogueMini()
    {
        switch (currentSuitor)
        {
            case "Hephaestus":
                hephaestusDialogueMini.SetActive(false);
                break;
            case "Dionysus":
                dionysusDialogueMini.SetActive(false);
                break;
            case "Sphinx":
                sphinxDialogueMini.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void EndDatingGame()
    {
        FadeOut();
        Invoke("ResetDatingGameElements", fadeTime);
    }

    public void ResetDatingGameElements()
    {
        currentDialogueSetup = null;
        datingGameBackground.SetActive(false);
        darkBackground.SetActive(true);
        Invoke("ActivateNextScene", fadeTime);
        levelController.inDatingGame = false;
        FadeIn();
    }

    public void ActivateNextScene()
    {
        switch (currentSuitor)
        {
            case "Hephaestus":
                actionController.DeactivateAllActions();
                hephaestusFinishedAction.SetActive(true);
                break;
            case "Dionysus":
                actionController.DeactivateAllActions();
                dionysusFinishedAction.SetActive(true);
                break;
            case "Sphinx":
                actionController.DeactivateAllActions();
                sphinxFinishedAction.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void AllowPlayerMovement()
    {
        playerController.ChangeStateToIdle();
        levelController.inDatingGame = false;
    }

    public void FadeIn()
    {
        fadeController.FadeIn(fadeTime);
    }

    public void FadeOut()
    {
        fadeController.FadeOut(fadeTime);
    }

}
