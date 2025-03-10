using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public TextMeshProUGUI text;
    public MasterVolumeController masterVolumeController;

    public void UpdateMasterVolume()
    {
        Slider slider = GetComponent<Slider>();

        masterVolumeController.SetMasterVolume(slider.value);
    }

    public void UpdateSliderUI() // This is called in the inspector
    {
        var sliderValue = GetComponent<Slider>().value;
        int sliderValueInPercentage = Mathf.RoundToInt(sliderValue * 100);
        text.text = $"{sliderValueInPercentage}";
    }
}
