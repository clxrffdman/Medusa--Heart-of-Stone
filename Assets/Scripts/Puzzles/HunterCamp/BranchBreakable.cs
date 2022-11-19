using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchBreakable : MonoBehaviour
{
    private bool isBroken;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    public Sprite[] branchSprites;
    public NpcController[] npcControllers;
    public GameObject branchSfx;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isBroken)
        {
            isBroken = true;
            BreakBranch();
            MakeNpcInvestigate();
        }
    }

    private void BreakBranch()
    {
        spriteRenderer.sprite = branchSprites[1];
        boxCollider.enabled = false;
        PlayBranchSfx();
    }

    private void MakeNpcInvestigate()
    {
        for (int i = 0; i < npcControllers.Length; i++)
        {
            npcControllers[i].targetDestination = this.transform;
            npcControllers[i].ChangeStateToInvestigating();
        }
        
    }

    private void PlayBranchSfx()
    {
        Instantiate(branchSfx, transform.position, transform.rotation);
    }

}
