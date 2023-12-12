using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager = null;
    private AudioSource backgroundMusic;
    [SerializeField]
    private AudioClip bg1, bg2, bg3;
    private float masterVolume = 0.0f;
    private float musicVolume = 0.0f;
    private float SFXVolume = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (audioManager != this)
        {
            Destroy(gameObject);
            return;
        }
        backgroundMusic = GetComponent<AudioSource>();
        //backgroundMusic.volume = 0.0f;
        //StartCoroutine(Fade(true, backgroundMusic, 7.0f, 0.5f));
        //StartCoroutine(Fade(false, backgroundMusic, 7.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 7 && SceneManager.GetActiveScene().buildIndex <= 15)
        {
            backgroundMusic.clip = bg2;
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 16 && SceneManager.GetActiveScene().buildIndex <= 25)
        {
            backgroundMusic.clip = bg3;
        }
        else if (SceneManager.GetActiveScene().buildIndex > 25)
        {
            backgroundMusic.clip = backgroundMusic.clip;
        }
        else
        {
            backgroundMusic.clip = bg1;
        }

        if (backgroundMusic.volume <= 0)
        {
            //StartCoroutine(Fade(true, backgroundMusic, 7.0f, 0.5f));
            //StartCoroutine(Fade(false, backgroundMusic, 7.0f, 0.0f));
        }

        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
            /*StartCoroutine(Fade(true, backgroundMusic, 7.0f, 0.5f));
            StartCoroutine(Fade(false, backgroundMusic, 7.0f, 0.0f));*/
        }

    }

    // FadeIn = true, FadeOut = false
    private IEnumerator Fade(bool fadeIn, AudioSource audio, float fadeDuration, float targetVolume)
    {
        // FadeOut, wait until the end of audio source to execute
        if (!fadeIn)
        {
            double lengthOfSource = (double)(audio.clip.samples / audio.clip.frequency);
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - fadeDuration));
        }

        float fadeTimer = 0.0f;
        float startVolume = audio.volume;
        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, targetVolume, fadeTimer / fadeDuration);
            yield return null;
        }

        yield break;
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
    }
    public float GetMusicVolume()
    {
        return musicVolume;
    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }
    public float GetSFXVolume()
    {
        return SFXVolume;
    }
    public void SetSFXVolume(float volume)
    {
        SFXVolume = volume;
    }
}
