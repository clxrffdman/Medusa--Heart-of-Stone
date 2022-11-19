using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{

    private bool isQuitting;
    public LevelControl levelControl;
    public GameObject devOptionsMenu;
    public bool devOptionsEnabled;
    public GameObject MainPauseScreen;
    public GameObject OptionScreen;
    public GameObject QuitScreen;
    public GameObject confirmButtonSfx;
    public GameObject backButtonSfx;

    private void OnEnable()
    {
        DisplayMainPauseScreen();
    }

    void Start()
    {
        if(levelControl == null)
        {
            levelControl = Resources.FindObjectsOfTypeAll<LevelControl>()[0];
        }
    }

    void Update()
    {

    }

    public void DisplayDevOptions()
    {
        if (!devOptionsEnabled)
        {
            devOptionsEnabled = true;
            devOptionsMenu.SetActive(true);
        }
        else
        {
            devOptionsEnabled = false;
            devOptionsMenu.SetActive(false);
        }
    }

    public void DevSaveData()
    {
        levelControl.SaveAllGameData();
        print("DEBUG: GAME SAVED");
    }

    public void DevLoadData()
    {
        levelControl.LoadScene();
        levelControl.LoadPlayer();
        print("LOADED PLAYER");
    }

    public void DevResetGameSaveData()
    {
        levelControl.ResetGameSaveData();
        print("RESETTING GAME DATA");
    }

    public void DevSetTaskList()
    {
        levelControl.SetTaskList();
        print("SETTING TASK LIST");
    }

    public void DevSetAllTasksAsCompleted()
    {
        levelControl.SetAllTasksAsCompleted();
        print("SETTING ALL TASKS AS COMPLETE");
    }

    public void DevResetAllTasks()
    {
        levelControl.ResetAllTasks();
        print("RESETTING ALL TASKS");
    }

    public void StartQuit()
    {
        if (isQuitting) { return; }
        isQuitting = true;
        PlayConfirmButtonSfx();
        StartCoroutine(ExitToMainMenu());
    }

    IEnumerator ExitToMainMenu()
    {
        yield return new WaitForSecondsRealtime(1f);

        TaskManager.Instance.taskList.Clear();
        GameManager.Instance.ResetGameManagerStates();
        SceneManager.LoadScene("StartScreen");
    }

    public void Resume()
    {
        GameManager.Instance.PublicPause(true);
    }

    public void DisplayMainPauseScreen()
    {
        if (isQuitting) { return; }
        MainPauseScreen.SetActive(true);
        OptionScreen.SetActive(false);
        QuitScreen.SetActive(false);
    }

    public void DisplayOptionScreen()
    {
        if (isQuitting) { return; }
        MainPauseScreen.SetActive(false);
        OptionScreen.SetActive(true);
        QuitScreen.SetActive(false);
    }

    public void DisplayQuitScreen()
    {
        if (isQuitting) { return; }
        MainPauseScreen.SetActive(false);
        OptionScreen.SetActive(false);
        QuitScreen.SetActive(true);
    }

    public void PlayConfirmButtonSfx()
    {
        Instantiate(confirmButtonSfx, transform.position, transform.rotation);
    }

    public void PlayBackButtonSfx()
    {
        if (isQuitting) { return; }
        Instantiate(backButtonSfx, transform.position, transform.rotation);
    }
}
