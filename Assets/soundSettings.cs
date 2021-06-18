using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundSettings : MonoBehaviour
{
    //SLIDERS
    [SerializeField]
    private Slider MusicVolumeSlider;
    [SerializeField]
    private Slider VFXVolumeSlider;

    void Start()
    {
        if (AudioManager.Instance.settings)
        {
            MusicVolumeSlider.value = AudioManager.Instance.OverallVolume_Music;
            VFXVolumeSlider.value = AudioManager.Instance.OverallVolume_SFX;
        }
        else
        {
            MusicVolumeSlider.value = 1f;
            VFXVolumeSlider.value = 1f;
        }

        //Adds listeners to the sliders and invokes a method when the value changes.
        MusicVolumeSlider.onValueChanged.AddListener(delegate { MusicValueChangeCheck(); });
        VFXVolumeSlider.onValueChanged.AddListener(delegate { VFXValueChangeCheck(); });
    }

    private void MusicValueChangeCheck()
    {
        AudioManager.Instance.ChangeMusicVolume(MusicVolumeSlider.value);
    }

    private void VFXValueChangeCheck()
    {
        AudioManager.Instance.ChangeSFXVolume(VFXVolumeSlider.value);
    }
}