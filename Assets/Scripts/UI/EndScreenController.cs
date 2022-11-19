using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    private FadeController fadeController;
    private bool isLoadingStartScreen;
    public float fadeTime;
    public string levelMusic;
    public string levelToLoad;

    void Start()
    {

        fadeController = GetComponent<FadeController>();

        fadeController.SetScreenToBlack();
        fadeController.FadeIn(fadeTime);

        SoundManager.Instance.StopAllMusic();

        if (SoundManager.Instance.currentMusicPlaying != levelMusic && !string.IsNullOrEmpty(levelMusic))
        {
            SoundManager.Instance.FadeAndPlayMusic(levelMusic, 1f);
        }
    }

    public void ReturnToMainMenu()
    {
        if (!isLoadingStartScreen)
        {
            isLoadingStartScreen = true;
            StartLoadingStartScreen();
        }
    }

    private void StartLoadingStartScreen()
    {
        fadeController.FadeOut(fadeTime);
        Invoke("LoadStartScreen", fadeTime);
    }

    private void LoadStartScreen()
    {
        SceneManager.LoadScene(this.levelToLoad);
    }

}
