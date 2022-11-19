using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesRustling : MonoBehaviour
{
    private bool isRustling;
    private Animator animatorLeaves;
    private BoxCollider2D boxCollider;

    public NpcController[] npcControllers;
    public GameObject leavesSfx;

    void Start()
    {
        animatorLeaves = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isRustling)
        {
            RustleLeaves();
            MakeNpcInvestigate();
        }
    }

    private void RustleLeaves()
    {
        isRustling = true;
        animatorLeaves.SetBool("isRustling", true);
        PlayLeavesSfx();
    }

    private void MakeNpcInvestigate()
    {
        for (int i = 0; i < npcControllers.Length; i++)
        {
            npcControllers[i].targetDestination = this.transform;
            npcControllers[i].ChangeStateToInvestigating();
        }
    }

    public void ResetRustlingLeaves()
    {
        isRustling = false;
        animatorLeaves.SetBool("isRustling", false);
    }

    private void PlayLeavesSfx()
    {
        Instantiate(leavesSfx, transform.position, transform.rotation);
    }
}