using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Audio;

public class SettingsMenuBehavior : MonoBehaviour
{
    public AudioSource masterChannel;
    public AudioSource musicChannel;
    public AudioSource SFXChannel;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMasterVolume(float volume)
    {
        //masterChannel.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        //musicChannel.SetFloat("volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        //SFXChannel.SetFloat("volume", volume);
    }
}
