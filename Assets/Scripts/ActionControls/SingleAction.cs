using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using Cinemachine;


public class SingleAction : MonoBehaviour
{
    private LevelControl levelController;
    private DialogueManager dialogueManager;
    private DialogueSetup dialogueSetup;
    private ActionController actionController;
    private bool isPaused = false;
    private bool isFading = false;
    private PlayerController playerController;
    private StateSetter stateSetter;
    private CameraShake cameraShake;

    [HideInInspector]
    public bool isPlayingDialogue = false;

    private bool cameraHasReachedTargetDestination = true;
    private bool levelLoading;
    private FadeController fadeController;
    private CinemachineBrain cinemachineBrain;
    private CinemachineVirtualCamera playerCamera;

    public string sceneName;
    public bool sceneHasFinished = false;
    public float playerDelayBeforeMoving;
    public bool playerCanMove;
    public bool playerLaserEnabled;
    public float playerSpeed = 0f;
    public string[] previousLevels;
    public Transform[] playerStartPositions;
    public bool playerShouldFaceLeft;
    public Transform playerDestination;
    public float timeBeforePlayerCanMove = 0f;
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public GameObject[] enemiesToActivate;
    public GameObject[] enemiesToDeactivate;
    public GameObject[] npcsToMove;
    public Transform[] npcsTargetDestinations;

    public List<Task> tasksToSet;
    public bool startScreenOnBlack;
    public bool shouldFadeOut;
    public bool shouldFadeIn;
    public float fadeTime;
    public float blackTime;
    public float pausedTime;
    public GameObject newSpawnPoint;
    public bool setCameraToFollowPlayer;
    public float cameraMoveTime = 0;
    public CinemachineVirtualCamera virtualCamera;
    public float cameraShakeAmount;
    public bool startCameraShake;
    public bool stopCameraShake;
    public bool startParticleSystem;
    public bool stopParticleSystem;
    public ParticleSystem[] particleSystemToEffect;
    public string musicName;
    public float musicPauseBeforePlayTime;
    public float musicFadeOutTime;
    public bool onlyFadeMusic;
    public GameObject sfx;
    public bool doNotSaveOnLoad;
    public string levelToLoad;
    public SpecialAction specialAction;
    public GameObject nextActionToLoad;

    void Start()
    {
        sceneName = gameObject.name;
        print(sceneName);
        actionController = FindObjectOfType<ActionController>();
        fadeController = GetComponent<FadeController>();
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        levelController = FindObjectOfType<LevelControl>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueSetup = GetComponentInChildren<DialogueSetup>();
        playerController = FindObjectOfType<PlayerController>();
        stateSetter = GetComponent<StateSetter>();
        cameraShake = FindObjectOfType<CameraShake>();
        specialAction = GetComponent<SpecialAction>();

        ActivateAllObjects();
        DeactivateAllObjects();
        if (!playerController.isDead)
        {
            HandlePlayerAbilityToMove();
        }
        HandlePlayerAbilityToLaser();
        ActivateActionEnemies();
        DeactivateActionEnemies();
        PlacePlayerAtStartPosition();
        GameManager.Instance.currentLevel = levelController.levelName;
        SetPlayerSpeed();
        MovePlayerToDestination();
        MoveNpcsToDestinations();
        HandleFading();
        HandleCameraSwitching();
       
        if (pausedTime > 0)
        {
            isPaused = true;
            Invoke("UnPauseScene", pausedTime);
        }

        if (newSpawnPoint != null)
        {
            playerController.gameObject.transform.position = newSpawnPoint.transform.position;
        }

        if(!string.IsNullOrEmpty(musicName))
        {
            SoundManager.Instance.FadeAndPlayMusic(musicName, musicFadeOutTime);
        }

        if (onlyFadeMusic)
        {
            SoundManager.Instance.FadeOutMusic(musicName, musicFadeOutTime);
        }

        if (sfx != null)
        {
            GameObject sfxToPlay = Instantiate(sfx, transform.position, transform.rotation);
        }

        if (startCameraShake)
        {
            StartShakingCamera();
        }

        if (stopCameraShake)
        {
            StopShakingCamera();
        }

        if(dialogueSetup != null)
        {
            PlayDialogueImmediately();
        }

        if (tasksToSet.Count > 0)
        {
            foreach(Task t in tasksToSet) {
                TaskManager.Instance.SetTaskAsComplete(t.taskID, t.completed);
            }
            levelController.UpdateStatesForEnvironmentalObjects();
            levelController.SetStatesForObjectCheckTasks();
        }

        if(specialAction != null)
        {
            specialAction.PerformSpecialAction();
        }

        if (!string.IsNullOrEmpty(levelToLoad))
        {
            if (!doNotSaveOnLoad) { levelController.SaveAllGameData(); }
            
            LoadNextLevel();
        }

        if(stateSetter != null)
        {
            stateSetter.SetCurrentState();
        }

        if(particleSystemToEffect != null)
        {
            SetParticleSystems();
        }  
    }

    void Update()
    {
        if (!isPaused && !isFading && !isPlayingDialogue && CheckHasPlayerReachedFinalDestination() && CheckIfAllNPCsHasReachedFinalDestination())
        {
            LoadNextAction();
        }   
    }

    public void ActivateAllObjects()
    {
        if (objectsToActivate == null)
        {
            return;
        }

        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(true);
        }
    }

    public void DeactivateAllObjects()
    {
        if (objectsToDeactivate == null)
        {
            return;
        }

        for (int i = 0; i < objectsToDeactivate.Length; i++)
        {
            objectsToDeactivate[i].SetActive(false);
        }
    }

    public void ActivateActionEnemies()
    {
        if (enemiesToActivate.Length != 0)
        {
            for (int i = 0; i < enemiesToActivate.Length; i++)
            {
                EnemyController enemyController = enemiesToActivate[i].GetComponent<EnemyController>();
                enemyController.SetAStarDestination(playerController.gameObject.transform);
                enemyController.ActivateEnemy();
            }
        }
    }

    public void DeactivateActionEnemies()
    {
        if (enemiesToDeactivate.Length != 0)
        {
            for (int i = 0; i < enemiesToDeactivate.Length; i++)
            {
                EnemyController enemyController = enemiesToDeactivate[i].GetComponent<EnemyController>();
                enemyController.SetAStarDestination(playerController.gameObject.transform);
                enemyController.DeactivateEnemy();
            }
        }
    }

    public void PlacePlayerAtStartPosition()
    {
        if (playerStartPositions.Length <= 0)
        {
            return;
        }

        if(playerStartPositions.Length == 1 || GameManager.Instance.currentLevel == "")
        {
            playerController.gameObject.transform.position = playerStartPositions[0].position;

            if (playerShouldFaceLeft)
            {
                playerController.MakePlayerFaceLeft();
            }

            return;
        }

        for (int i = 0; i < previousLevels.Length; i++)
        {
            if(previousLevels[i] == GameManager.Instance.currentLevel)
            {
                playerController.gameObject.transform.position = playerStartPositions[i].position;

                if (playerShouldFaceLeft)
                {
                    playerController.MakePlayerFaceLeft();
                }
            }
        }
    }

    public void SetPlayerSpeed()
    {
        if (playerSpeed != 0f)
        {
            playerController.speed = playerSpeed;
        } else
        {
            playerController.speed = playerController.baseSpeed;
        }
    }

    public void MovePlayerToDestination()
    {
        if (playerDestination != null)
        {
            playerController.targetDestination = playerDestination;
            playerController.hasReachedDestination = false;
            playerController.ChangeStateToSceneControlled();
        }
    }

    public bool CheckHasPlayerReachedFinalDestination()
    {
        bool playerHasReachedDestination = true;

        if (!playerController.hasReachedDestination)
        {
            playerHasReachedDestination = false;
        }

        return playerHasReachedDestination;
    }

    public void MoveNpcsToDestinations()
    {
        if(npcsToMove.Length > 0)
        {
            for (int i = 0; i < npcsToMove.Length; i++)
            {
                NpcController npcController = npcsToMove[i].GetComponent<NpcController>();
                EnemyController enemyController = npcsToMove[i].GetComponent<EnemyController>();
                if (npcController != null)
                {
                    npcController.targetDestination = npcsTargetDestinations[i];
                    npcController.ChangeStateToSceneControlled();
                }

                if (enemyController != null)
                {
                    enemyController.isSceneControlled = true;
                    enemyController.SetAStarDestination(npcsTargetDestinations[i]);
                    enemyController.StartAStarPathfinding();
                }
            }
        }
    }

    public bool CheckIfAllNPCsHasReachedFinalDestination()
    {
        bool allNPCsHaveReachedFinalDestination = true;

        if(npcsToMove.Length > 0)
        {
            for (int i = 0; i < npcsToMove.Length; i++)
            {
                NpcController npcController = npcsToMove[i].GetComponent<NpcController>();
                EnemyController enemyController = npcsToMove[i].GetComponent<EnemyController>();
                if (npcController != null)
                {
                    if (!npcController.hasReachedDestination)
                    {
                        allNPCsHaveReachedFinalDestination = false;
                    }
                }

                if (enemyController != null)
                {
                    if (!enemyController.hasReachedDestination)
                    {
                        allNPCsHaveReachedFinalDestination = false;
                    }
                }
            }
        }
        return allNPCsHaveReachedFinalDestination;
    }

    public void LoadNextLevel()
    {
        //SaveSystem.SaveScene(GameObject.Find("Player").GetComponent<PlayerController>(), SaveGameManager.Instance.saveIndex);
        SaveSystem.SaveScene(FindObjectOfType<PlayerController>(), SaveGameManager.Instance.saveIndex);
        SaveGameManager.Instance.sceneToScene = true;
        SceneManager.LoadScene(this.levelToLoad);
    }

    public void UnPauseScene()
    {
        isPaused = false;
    }

    public void HandleFading()
    {
        if (startScreenOnBlack)
        {
            fadeController.SetScreenToBlack();
        }

        if (shouldFadeIn)
        {
            isFading = true;
            Invoke("FadeIn", blackTime);
        }
        else if (shouldFadeOut)
        {
            isFading = true;
            FadeOut();
        }
    }

    public void FadeIn()
    {
        fadeController.FadeIn(fadeTime);
        Invoke("EndFade", fadeTime);
    }

    public void FadeOut()
    {
        fadeController.FadeOut(fadeTime);
        Invoke("EndFade", fadeTime + blackTime);
    }

    public void EndFade()
    {
        isFading = false;
    }

    public void HandleCameraSwitching()
    {

        if (cameraMoveTime != 0)
        {
            //cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            cinemachineBrain.m_DefaultBlend.m_Time = cameraMoveTime;
        }

        if (setCameraToFollowPlayer)
        {
            SwitchToPlayerCamera();
        }

        if (virtualCamera != null)
        {
            SwitchVirtualCameras();
        }
    }

    public void SwitchVirtualCameras()
    {
        playerCamera.Priority = 0;
        virtualCamera.Priority = 10;
        actionController.currentCamera = virtualCamera;
    }

    public void SwitchToPlayerCamera()
    {
        actionController.currentCamera.Priority = 0;
        actionController.currentCamera = playerCamera;
        playerCamera.Priority = 10;
    }

    public void StartShakingCamera()
    {
        cameraShake.StartShakingCamera(cameraShakeAmount);
    }

    public void StopShakingCamera()
    {
        cameraShake.StopShakingCamera();
    }

    public void HandlePlayerAbilityToMove()
    {
        if (playerCanMove)
        {
            Invoke("AllowPlayerMovement", timeBeforePlayerCanMove);
        }
        else
        {
            playerController.ChangeStateToFrozen();
        }
    }

    public void HandlePlayerAbilityToLaser()
    {
        if (playerLaserEnabled)
        {
            playerController.laserEnabled = true;
        } else
        {
            playerController.laserEnabled = false;
        }
    }

    public void AllowPlayerMovement()
    {
        playerController.ChangeStateToIdle();
    }

    public void PlayDialogueImmediately()
    {
        isPlayingDialogue = true;
        dialogueManager.isInActionDialogue = true;
        dialogueManager.ClearAllDialogueFromList();
        dialogueSetup.ConstructDialogue();
        dialogueManager.currentDialogueSetup = dialogueSetup;
        dialogueManager.ActivateDialogue();
    }

    public void SetParticleSystems()
    {
        if (particleSystemToEffect.Length > 0)
        {
            if (startParticleSystem)
            {
                for (int i = 0; i < particleSystemToEffect.Length; i++)
                {
                    particleSystemToEffect[i].Play();
                }
            }

            if (stopParticleSystem)
            {
                for (int i = 0; i < particleSystemToEffect.Length; i++)
                {
                    particleSystemToEffect[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
                }
            }
        }
    }

    public void LoadNextAction()
    {
        if(nextActionToLoad != null)
        {
            nextActionToLoad.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}


