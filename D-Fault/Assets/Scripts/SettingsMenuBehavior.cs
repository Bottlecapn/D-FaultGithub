using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuBehavior : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;
    private AudioManager audioManager;

    private void Start()
    {
        GameObject AudioManager = GameObject.FindGameObjectWithTag("Audio");
        if (AudioManager != null)
        {
            audioManager = AudioManager.GetComponent<AudioManager>();
        }
        if (AudioManager != null)
        {
            masterVolumeSlider.value = audioManager.GetMasterVolume();
            musicVolumeSlider.value = audioManager.GetMusicVolume();
            SFXVolumeSlider.value = audioManager.GetSFXVolume();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMasterVolume(float volume)
    {
        audiomixer.SetFloat("Master Volume", volume);
        audioManager.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        audiomixer.SetFloat("Music Volume", volume);
        audioManager.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        audiomixer.SetFloat("SFX Volume", volume);
        audioManager.SetSFXVolume(volume);
    }
}
