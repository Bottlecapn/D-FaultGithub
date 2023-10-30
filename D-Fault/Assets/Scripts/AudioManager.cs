using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager = null;
    private AudioSource backgroundMusic;
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
        backgroundMusic.volume = 0.0f;
        StartCoroutine(Fade(true, backgroundMusic, 7.0f, 1.0f));
        StartCoroutine(Fade(false, backgroundMusic, 7.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
            StartCoroutine(Fade(true, backgroundMusic, 7.0f, 1.0f));
            StartCoroutine(Fade(false, backgroundMusic, 7.0f, 0.0f));
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
}