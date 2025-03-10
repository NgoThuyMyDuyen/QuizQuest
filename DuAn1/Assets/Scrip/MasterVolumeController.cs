using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MasterVolumeController : MonoBehaviour
{

    [Range(0f, 1f)] public float masterVolume;

    public Slider settingsSlider;

    private void Awake()
    {
        // Load the saved volume value and set it
        float savedVolume = LoadMasterVolume();
        SetMasterVolume(savedVolume);
    }

    private void Start() //Instead of Start so it will happen in every scene
    {
        SetSettingsSlider();
    }

    private float LoadMasterVolume()
    {
        // Get value from PlayerPrefs, return 1 (default volume) if not found
        return PlayerPrefs.GetFloat("masterVolume", 1);
    }

    public void SetMasterVolume(float volume)
    {
        // Set the master volume and save the value
        masterVolume = volume;
        AudioListener.volume = masterVolume;
    }

    public void SetSettingsSlider()
    {
        if (settingsSlider != null)
        {
            settingsSlider.value = masterVolume;
            settingsSlider.GetComponent<VolumeSlider>().UpdateSliderUI();
        }
        else
        {
            Debug.LogWarning("Settings slider not found");
        }
    }

    public void SaveMasterVolume()
    {
        Debug.Log("Saved master volume : " + masterVolume);
        PlayerPrefs.SetFloat("masterVolume", masterVolume); // saving 0.0- 1.0
    }
}