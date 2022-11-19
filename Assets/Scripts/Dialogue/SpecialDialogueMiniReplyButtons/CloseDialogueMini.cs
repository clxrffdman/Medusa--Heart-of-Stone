using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseDialogueMini : DialgoueMiniReplyButton
{

    private Button[] replyButtons;
    public DialogueMini dialogueMini;
    public GameObject replyHolder;

    public void OnEnable()
    {
        replyButtons = replyHolder.GetComponentsInChildren<Button>();
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].interactable = true;
        }
    }

    public void CloseDialogueMiniBox()
    {
        dialogueMini.EndDialogue();
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].interactable = false;
        }
    }
}
