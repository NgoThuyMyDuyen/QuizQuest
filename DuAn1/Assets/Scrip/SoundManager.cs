using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region || -- Singelton -- ||
    public static SoundManager Instance {get; set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(Instance);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion
    [Header ("SFX Clips")]
    public AudioClip timeSound;
    public AudioClip wrongAnswerSound;
    public AudioClip correctAnswerSound;
    public AudioClip gameOverSound;

    [Header ("Background Music Clips")]
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    [Header ("Audio Sources")]
    public AudioSource bgMusicSource;
    public AudioSource sfxSource;

    public void PlayBGMusic (AudioClip clip, float volume = 1f)
    {
        if (bgMusicSource.isPlaying == false)
        {
            bgMusicSource.clip = clip;
            bgMusicSource.volume = volume;
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }

    public void StopBGMusic()
    {
        if (bgMusicSource != null)
        {
            bgMusicSource.Stop();
        }
    }

    public void PlayTimerSFX()
    {
        sfxSource.PlayOneShot(timeSound);
    }

    public void PlayCorrectAnswerSFX()
    {
        sfxSource.PlayOneShot(correctAnswerSound);
    }

    public void PlayWrongAnswerSFX()
    {
        sfxSource.PlayOneShot(wrongAnswerSound);
    }

    public void PlayGameOverSFX()
    {
        sfxSource.PlayOneShot(gameOverSound);
    }
}
