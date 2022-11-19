using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    public GameObject[] Saves;
    public bool shownSaves;
    public GameObject[] textFields;
    public GameObject[] saveFileButtons;
    public TextMeshProUGUI text;
    public string levelMusic;
    public string sceneToLoad;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject saveMenu;

    public Button createNewSaveButtonLarge;
    public Button createNewSaveButtonSmall;
    public GameObject enterNameTitle;
    public GameObject verticalLine;
    public Button loadButton01;
    public TextMeshProUGUI saveDataTitle01;

    public GameObject buttonConfirmSfx;
    public GameObject buttonBackSfx;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        var folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        var save0Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save0");
        var save1Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save1");
        var save2Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save2");
        var save3Folder = Directory.CreateDirectory(Application.persistentDataPath + "/saves/save3");
        RefreshSaveNames();

        Time.timeScale = 1;
        GameManager.Instance.pauseMenuActive = false;
        TaskManager.Instance.taskList.Clear();

        SoundManager.Instance.StopAllMusic();
        SoundManager.Instance.StopCurrentlyRunningCoroutines();
        if (SoundManager.Instance.currentMusicPlaying != levelMusic && !string.IsNullOrEmpty(levelMusic))
        {
            SoundManager.Instance.FadeAndPlayMusic(levelMusic, 0f);
        }

        
        ActivateMainMenu();
    }

    void Update()
    {
        
    }

    public void RefreshSaveNames()
    {
        loadButton01.gameObject.SetActive(false);
        createNewSaveButtonLarge.gameObject.SetActive(true);
        createNewSaveButtonSmall.gameObject.SetActive(false);
        enterNameTitle.SetActive(false);
        verticalLine.SetActive(false);
        saveFileButtons[0].SetActive(false);
        textFields[0].SetActive(false);
        saveDataTitle01.gameObject.SetActive(false);

        if (SaveSystem.LoadPlayer(1) != null)
        {
            PlayerData data1 = SaveSystem.LoadPlayer(1);
            saveDataTitle01.gameObject.SetActive(true);
            saveDataTitle01.text = data1.saveName;
            loadButton01.gameObject.SetActive(true);
            createNewSaveButtonLarge.gameObject.SetActive(false);
            createNewSaveButtonSmall.gameObject.SetActive(true);
            verticalLine.SetActive(true);
            saveFileButtons[0].SetActive(false);
            textFields[0].SetActive(false);
            SetButtonFocus();
        }

        //if (SaveSystem.LoadPlayer(2) != null)
        //{
        //    PlayerData data2 = SaveSystem.LoadPlayer(2);
        //    Saves[1].transform.GetChild(Saves[1].transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = data2.saveName;
        //    Saves[1].transform.GetChild(Saves[1].transform.childCount - 2).gameObject.SetActive(true);
        //}

        //if (SaveSystem.LoadPlayer(3) != null)
        //{
        //    PlayerData data3 = SaveSystem.LoadPlayer(3);
        //    Saves[2].transform.GetChild(Saves[2].transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = data3.saveName;
        //    Saves[2].transform.GetChild(Saves[2].transform.childCount - 2).gameObject.SetActive(true);
        //}

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void FullscreenToggle()
    {
        if (GameManager.Instance.isInFullscreen)
        {
            Screen.fullScreen = false;
            GameManager.Instance.isInFullscreen = false;
        }
        else
        {
            Screen.fullScreen = true;
            GameManager.Instance.isInFullscreen = true;
        }
    }

    public void Override1()
    {
        GameManager.Instance.saveIndex = 1;
        GameManager.Instance.loaded = false;
        GameManager.Instance.freshSave = true;
        SaveSystem.ClearSaveSlot(1);
        Invoke("PlayGame", 0.2f);
       

    }

    public void Override2()
    {
        GameManager.Instance.saveIndex = 2;
        GameManager.Instance.loaded = false;
        GameManager.Instance.freshSave = true;
        SaveSystem.ClearSaveSlot(2);
        Invoke("PlayGame", 0.2f);

    }

    public void Override3()
    {
        GameManager.Instance.saveIndex = 3;
        GameManager.Instance.loaded = false;
        GameManager.Instance.freshSave = true;
        SaveSystem.ClearSaveSlot(3);
        Invoke("PlayGame", 0.2f);

    }

    public void LoadPlayer1()
    {
        PlayerData data = SaveSystem.LoadPlayer(1);
        GameManager.Instance.saveIndex = 1;
        GameManager.Instance.fileSaveName = data.saveName;
        GameManager.Instance.loaded = true;
        SceneManager.LoadScene(data.sceneIndex);

    }

    public void LoadPlayer2()
    {
        PlayerData data = SaveSystem.LoadPlayer(2);
        GameManager.Instance.saveIndex = 2;
        GameManager.Instance.loaded = true;
        GameManager.Instance.fileSaveName = data.saveName;
        SceneManager.LoadScene(data.sceneIndex);

    }

    public void LoadPlayer3()
    {
        PlayerData data = SaveSystem.LoadPlayer(3);
        GameManager.Instance.saveIndex = 3;
        GameManager.Instance.loaded = true;
        GameManager.Instance.fileSaveName = data.saveName;
        SceneManager.LoadScene(data.sceneIndex);
    }

    public void SaveName1()
    {
        saveDataTitle01.gameObject.SetActive(false);
        loadButton01.gameObject.SetActive(false);
        createNewSaveButtonLarge.gameObject.SetActive(false);
        createNewSaveButtonSmall.gameObject.SetActive(false);
        enterNameTitle.SetActive(true);
        verticalLine.SetActive(true);
        textFields[0].SetActive(true);
        saveFileButtons[0].SetActive(true);
        TMP_InputField inputField = textFields[0].GetComponent<TMP_InputField>();
        inputField.Select();
    }

    public void SaveName2()
    {
        textFields[1].SetActive(true);
        saveFileButtons[1].SetActive(true);
        textFields[1].GetComponent<TMP_InputField>().Select();
    }

    public void SaveName3()
    {
        textFields[2].SetActive(true);
        saveFileButtons[2].SetActive(true);
        textFields[2].GetComponent<TMP_InputField>().Select();
    }

    public void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        saveMenu.SetActive(false);
    }

    public void ActivateOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        saveMenu.SetActive(false);
    }

    public void ActivateSaveMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        saveMenu.SetActive(true);
        RefreshSaveNames();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void SetButtonFocus()
    {
        if (loadButton01.gameObject.activeInHierarchy)
        {
            loadButton01.Select();
            loadButton01.OnSelect(null);
            return;
        }

        if (createNewSaveButtonLarge.gameObject.activeInHierarchy)
        {
            createNewSaveButtonLarge.Select();
            createNewSaveButtonLarge.OnSelect(null);
        }
    }

    public void PlayButtonConfirmSfx()
    {
        Instantiate(buttonConfirmSfx, transform.position, transform.rotation);
    }

    public void PlayButtonBackSfx()
    {
        Instantiate(buttonBackSfx, transform.position, transform.rotation);
    }
}
