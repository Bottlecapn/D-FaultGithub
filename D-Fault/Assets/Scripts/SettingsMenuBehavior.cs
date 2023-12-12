using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Audio;
using UnityEngine.Audio;

public class SettingsMenuBehavior : MonoBehaviour
{
    public AudioMixer audiomixer;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMasterVolume(float volume)
    {
        audiomixer.SetFloat("Master Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audiomixer.SetFloat("Music Volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audiomixer.SetFloat("SFX Volume", volume);
    }
}
