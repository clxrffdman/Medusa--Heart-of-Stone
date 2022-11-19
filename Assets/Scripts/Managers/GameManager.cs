using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public bool isInDevelopmentMode;
    public bool isInFullscreen;
    public bool isPaused;
    public bool inventoryActive;
    public bool pauseMenuActive;
    public bool dialogueActive;

    public GameObject openPauseMenuSfx;
    public GameObject closePauseMenuSfx;
    public GameObject pauseUI;
    public AudioMixer mainAudioMixer;

    public string currentLevel;
    public string fileSaveName;
    public int saveIndex;
    public bool loaded;
    public bool freshSave;
    public bool sceneToScene;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            if(Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Start()
    {
        if(pauseUI == null)
        {
            if(Resources.FindObjectsOfTypeAll<PauseUI>() != null)
            {
                PauseUI[] allPauseUI = Resources.FindObjectsOfTypeAll<PauseUI>();
                if(allPauseUI.Length > 0)
                {
                    pauseUI = allPauseUI[0].gameObject;
                }
            }
        }

        SetAudioLevels();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inventoryActive)
        {
            PublicPause(pauseMenuActive);
        }

        if (Input.GetKeyDown(KeyCode.R) && isInDevelopmentMode)
        {
            Debug.Break();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void PublicPause(bool isPauseActive)
    {
        if (!isPauseActive)
        {
            pauseUI.SetActive(true);
            pauseMenuActive = true;
            //AudioSource[] audioArray = GameObject.FindObjectsOfType<AudioSource>();
            //foreach (AudioSource a in audioArray)
            //{
            //    a.Pause();
            //}
            var pauseSound = Instantiate(openPauseMenuSfx, transform.position, Quaternion.identity);
            pauseSound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/SFX/miscSound");
            PauseGame();
        }
        else
        {
            pauseUI.SetActive(false);
            pauseMenuActive = false;
            //AudioSource[] audioArray = GameObject.FindObjectsOfType<AudioSource>();
            //foreach (AudioSource a in audioArray)
            //{
            //    a.UnPause();
            //}
            var pauseSound = Instantiate(closePauseMenuSfx, transform.position, Quaternion.identity);
            pauseSound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/SFX/miscSound");
            UnPauseGame();
        }

        
    }

    public void ResetGameManagerStates()
    {
        isPaused = false;
        inventoryActive = false;
        pauseMenuActive = false;
        dialogueActive = false;
        currentLevel = "";
    }

    public void SetAudioLevels()
    {
        float musicVolume = Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20;
        float sfxVolume = Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20;
        mainAudioMixer.SetFloat("MusicVolume", musicVolume);
        mainAudioMixer.SetFloat("SFXVolume", sfxVolume);
    }

    

}
