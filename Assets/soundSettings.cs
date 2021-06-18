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