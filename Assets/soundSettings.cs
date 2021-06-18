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

    //VALUES
    //public float MasterVolumeSet;
    //public float MusicVolumeSet;
    //public float VFXVolumeSet;

    void Start()
    {
        //MasterVolumeSet = 50f;
        //MusicVolumeSet = 100f;
        //VFXVolumeSet = 100f;

        //Adds a listener to the main slider and invokes a method when the value changes.
        //MasterVolumeSlider.onValueChanged.AddListener(delegate { MasterValueChangeCheck(); });
        MusicVolumeSlider.onValueChanged.AddListener(delegate { MusicValueChangeCheck(); });
        VFXVolumeSlider.onValueChanged.AddListener(delegate { VFXValueChangeCheck(); });
    }

    private void MasterValueChangeCheck()
    {
        //MasterVolumeSet = MasterVolumeSlider.value;
    }

    private void MusicValueChangeCheck()
    {
        //MusicVolumeSet = MusicVolumeSlider.value;
        Debug.Log(MusicVolumeSlider.value);
        AudioManager.Instance.ChangeMusicVolume(MusicVolumeSlider.value);
    }

    private void VFXValueChangeCheck()
    {
        //VFXVolumeSet = VFXVolumeSlider.value;
        Debug.Log(VFXVolumeSlider.value);
        AudioManager.Instance.ChangeSFXVolume(VFXVolumeSlider.value);
    }
}