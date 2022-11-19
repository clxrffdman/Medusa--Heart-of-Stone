using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueMini : MonoBehaviour
{

    private PlayerController playerController;
    private bool canBeInteractedWith;
    private bool isOpening;
    private AudioSource audioSource;
    private bool isReplying;
    private DialogueMiniLine currentMiniDialogueLine;
    private LevelControl levelController;
    private int dialogueMiniLineIndex = 0;
    public bool dialogueActive { get; private set; }
    public bool isTyping { get; private set; }

    public DialogueMiniLine[] miniDialogues;

    [Header("Settings")]
    [SerializeField] private float typeSpeed;
    [SerializeField] private AudioClip typeSound;

    public float typingSpeed = 0.01f;
    public float typeSoundSpeed = 0.1f;
    public float typeSoundVolume = 0.2f;

    [Header("Objects")]
    public GameObject dialogueMiniHolder;
    public GameObject replyHolder;
    public Button replyButton;
    public Animator animator;
    public Text dialogueText;
    public AudioClip maleVoice;
    public AudioClip femaleVoice;
    public AudioClip genericVoice;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        animator = dialogueMiniHolder.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        levelController = FindObjectOfType<LevelControl>();
        dialogueMiniHolder.SetActive(false);
        replyHolder.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canBeInteractedWith = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canBeInteractedWith = false;
        }
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !isOpening && canBeInteractedWith)
        {
            if (dialogueActive && !isReplying)
            {
                if (isTyping)
                {
                    FastForwardDialogue();
                }
                else
                {
                    DisplayNextDialogueLine();
                }
            }

            if (!dialogueActive && !levelController.inDialogue && !levelController.inDialogueMini)
            {
                DialogueMini closestDialogueMini = FindClosestDialogueMini();
                if(closestDialogueMini == this)
                {
                    ActivateDialogue();
                }
            }
        }
    }

    public void ActivateDialogue()
    {
        dialogueActive = true;
        dialogueText.text = "";
        playerController.ChangeStateToFrozen();
        currentMiniDialogueLine = miniDialogues[0];
        StartOpeningDialogueHolder();
        MakeSpeakersFaceEachOther();
        OverrideFacingDirection();
    }

    public void StartDialogue()
    {
        isOpening = false;
        Dialogue();
    }

    public void Dialogue()
    {
        dialogueText.text = "";
        OverrideFacingDirection();
        if (currentMiniDialogueLine.showReplies)
        {
            dialogueText.text = currentMiniDialogueLine.spokenWords;
            replyHolder.SetActive(true);
            isReplying = true;
        }

        if (!isReplying)
        {
            StartCoroutine(TypeSentence(currentMiniDialogueLine.spokenWords));
        }
    }

    public DialogueMini FindClosestDialogueMini()
    {
        DialogueMini closestDialogueMini = null;
        DialogueMini[] allDialogueMinis = FindObjectsOfType<DialogueMini>();
        float currentClosestDistance = 10000;

        for (int i = 0; i < allDialogueMinis.Length; i++)
        {
            if (currentClosestDistance == 10000)
            {
                currentClosestDistance = Vector3.Distance(playerController.gameObject.transform.position, allDialogueMinis[i].gameObject.transform.position);
                closestDialogueMini = allDialogueMinis[i];
            }
            else
            {
                float distance = Vector3.Distance(playerController.gameObject.transform.position, allDialogueMinis[i].gameObject.transform.position);
                if (distance < currentClosestDistance && allDialogueMinis[i].gameObject.activeInHierarchy == true)
                {
                    currentClosestDistance = distance;
                    closestDialogueMini = allDialogueMinis[i];
                }
            }
        }

        return closestDialogueMini;
    }

    public void MakeSpeakersFaceEachOther()
    {
        if (this.gameObject.transform.position.x > playerController.gameObject.transform.position.x)
        {
            playerController.MakePlayerFaceRight();
        }
        else
        {
            playerController.MakePlayerFaceLeft();
        }
    }

    public void OverrideFacingDirection()
    {

    }

    public void SetSpeakerVoice()
    {
        if (!string.IsNullOrEmpty(currentMiniDialogueLine.voice))
        {
            switch (currentMiniDialogueLine.voice)
            {
                case "male":
                    audioSource.clip = maleVoice;
                    break;
                case "female":
                    audioSource.clip = femaleVoice;
                    break;
                case "generic":
                    audioSource.clip = genericVoice;
                    break;
                default:
                    break;
            }
        }
    }


    public void DisplayNextDialogueLine()
    {
        dialogueMiniLineIndex++;
        if(dialogueMiniLineIndex >= miniDialogues.Length)
        {
            EndDialogue();
        } else
        {
            currentMiniDialogueLine = miniDialogues[dialogueMiniLineIndex];
            Dialogue();
        }  
    }

    public IEnumerator TypeSentence(string spokenWords)
    {
        isTyping = true;
        int characterIndex = 0;
        SetSpeakerVoice();

        if (spokenWords.Length > 0)
        {
            StartCoroutine(PlayTypingSound());
        }

        for (int i = 0; i <= spokenWords.Length - 1; i++)
        {
            characterIndex++;
            string text = spokenWords.Substring(0, characterIndex);
            text += "<color=#00000000>" + spokenWords.Substring(characterIndex) + "</color>";
            dialogueText.text = text;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }

        isTyping = false;
    }

    IEnumerator PlayTypingSound()
    {
        audioSource.Play();
        yield return new WaitForSeconds(typeSoundSpeed);
        if (isTyping)
        {
            StartCoroutine(PlayTypingSound());
        }
    }

    public void FastForwardDialogue()
    {
        StopAllCoroutines();
        isTyping = false;
        dialogueText.text = currentMiniDialogueLine.spokenWords;
    }

    public void EndDialogue()
    {
        StartClosingDialogueHolder();
    }

    public void CloseDialogueHolder()
    {

        ResetDialogueMini();
        dialogueMiniHolder.SetActive(false);
        playerController.ChangeStateToIdle();
    }

    public void ResetDialogueMini()
    {
        dialogueMiniLineIndex = 0;
        dialogueActive = false;
        isReplying = false;
        replyHolder.SetActive(false);
    }

    public void StartOpeningDialogueHolder()
    {
        dialogueMiniHolder.SetActive(true);
        animator.SetBool("isClosing", false);
        isOpening = true;
        Invoke("StartDialogue", 0.5f);
    }

    public void StartClosingDialogueHolder()
    {
        animator.SetBool("isClosing", true);
        Invoke("CloseDialogueHolder", 0.5f);
    }

    [System.Serializable]
    public class DialogueMiniLine
    {
        public string speakerName;
        public string spokenWords;
        public string voice;
        public bool showReplies;
    }
}
