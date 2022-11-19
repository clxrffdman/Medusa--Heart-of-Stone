using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDatingGame : DialgoueMiniReplyButton
{

    private Button[] replyButtons;
    public DialogueMini dialogueMini;
    public GameObject replyHolder;
    public DatingGameController datingGameController;
    public string suitorName;

    public void OnEnable()
    {
        replyButtons = replyHolder.GetComponentsInChildren<Button>();
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].interactable = true;
        }
    }

    public void StartDatingGame()
    {
        dialogueMini.EndDialogue();
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].interactable = false;
        }
        datingGameController.StartDatingGame(suitorName);
    }
}
