using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class SpotlightController : MonoBehaviour
{
    private ActionController actionController;

    public GameObject player;
    public bool isVisible;
    public bool isSpotted;
    public bool failed;
    public float defaultZoom, moddedZoom;
    public GameObject actionToActivate;
    
    public List<CoverInstance> medusaCover;
    public List<SpotlightDetect> spotlights;
    public NpcController[] hunterNpcs;
    public NpcPatrol[] npcPatrols;
    public CinemachineVirtualCamera playerCam;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        hunterNpcs = FindObjectsOfType<NpcController>();
        npcPatrols = FindObjectsOfType<NpcPatrol>();
        actionController = FindObjectOfType<ActionController>();

        failed = false;
        defaultZoom = playerCam.m_Lens.OrthographicSize; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateVisibility();
        CheckSpotted();
        if (isSpotted && !failed)
        {
            failed = true;
            RestartLevel();
        }

    }

    private void RestartLevel()
    {
        StopAllSpotlightMovement();
        StopAllNpcPatrolling();
        MakeNpcsCatchPlayer();
        actionController.DeactivateAllActions();
        actionToActivate.SetActive(true);
    }

    public IEnumerator FailSequence()
    {
        LeanTween.value(gameObject, defaultZoom, moddedZoom, 0.25f).setOnUpdate((float val) =>
        {
            playerCam.m_Lens.OrthographicSize = val;
        });
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void UpdateVisibility()
    {
        isVisible = true;
        foreach (CoverInstance go in medusaCover)
        {
            if (go.isBehind == true)
            {
                isVisible = false;
            }
        }
    }

    public void CheckSpotted()
    {
        if(spotlights.Count <= 0) { return; }

        if (!isSpotted)
        {
            foreach (SpotlightDetect st in spotlights)
            {
                if (isVisible && st.spotted)
                {
                    isSpotted = true;
                }
            }
        } 
    }

    public void MakeNpcsCatchPlayer()
    {
        for (int i = 0; i < hunterNpcs.Length; i++)
        {
            print("making soldiers catch player.");
            hunterNpcs[i].ChangeStateToFrozen();
            hunterNpcs[i].MakeNpcFacePlayer();
        }
    }

    public void StopAllNpcPatrolling()
    {
        for (int i = 0; i < npcPatrols.Length; i++)
        {
            npcPatrols[i].StopPatrolling();
        }
    }

    public void StopAllSpotlightMovement()
    {
        for (int i = 0; i < spotlights.Count; i++)
        {
            spotlights[i].StopSpotlightMovement();
        }
    }
}
