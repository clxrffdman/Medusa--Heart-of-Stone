using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    public int index;

    private void OnEnable()
    {
        if (index == 1)
        {
            float playerPrefsMusic = (PlayerPrefs.GetFloat("MusicVolume"));
            if(playerPrefsMusic == 0f)
            {
                slider.value = 0.8f;
            }
            else
            {
                slider.value = playerPrefsMusic;
            }
        }

        if(index == 2)
        {
            float playerPrefsSfx = (PlayerPrefs.GetFloat("SFXVolume"));
            if (playerPrefsSfx == 0f)
            {
                slider.value = 0.8f;
            }
            else
            {
                slider.value = playerPrefsSfx;
            }
        }
    }

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMusicLevels(float sliderValue)
    {
        float volumeLevel = Mathf.Log10(sliderValue) * 20;
        GameManager.Instance.mainAudioMixer.SetFloat("MusicVolume", volumeLevel);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void SetSFXLevels(float sliderValue)
    {
        float volumeLevel = Mathf.Log10(sliderValue) * 20;
        GameManager.Instance.mainAudioMixer.SetFloat("SFXVolume", volumeLevel);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        PlayerPrefs.Save();
    }
}
