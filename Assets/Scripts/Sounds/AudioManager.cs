using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioFile[] AudioFiles;
    public AudioFile[] MusicFiles => AudioFiles.Where(x => x.Type == AudioType.Music).ToArray();
    public AudioSource AudioSource_Music;
    public AudioSource AudioSource_SFX;
    public float OverallVolume_SFX;
    public float OverallVolume_Music;

    public AudioMixer AudioMixer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        ChangeMusicVolume(OverallVolume_Music);
        ChangeSFXVolume(OverallVolume_SFX);
    }

    public static void PlayMusic(string name)
    {
        Instance._PlayMusic(name);
    }

    public static void PlaySFX(string name)
    {
        Instance._PlaySFX(name);
    }

    private AudioFile GetFileByName(string soundName)
    {
        return AudioFiles.First(x => x.Name == soundName);
    }

    public void _PlaySFX(string soundName)
    {
        var file = GetFileByName(soundName);

        if (file != null)
        {
            var clip = file.Clip;
            AudioSource_SFX.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                    AudioMixer.FindMatchingGroups(name)[0] :
                    AudioMixer.FindMatchingGroups("SFX")[0];
            AudioSource_SFX.volume = OverallVolume_SFX;
            AudioSource_SFX.clip = clip;
            AudioSource_SFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Trying to play sound that does no exist, merluzo! " + soundName);
        }
    }
    public void _PlayMusic(string soundName, bool looping = false)
    {
        var file = GetFileByName(soundName);

        if (file != null)
        {
            var clip = file.Clip;
            if (clip != null)
            {
                AudioSource_Music.clip = clip;
                AudioSource_Music.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                    AudioMixer.FindMatchingGroups(name)[0] :
                    AudioMixer.FindMatchingGroups("Music")[0];
                AudioSource_Music.volume = OverallVolume_Music;
                AudioSource_Music.Play();
                AudioSource_Music.loop = looping;
            }
        }
        else
        {
            Debug.LogError("Trying to play sound that does no exist, merluzo! " + soundName);
        }
    }

    public void _StopMusic()
    {
        AudioSource_Music.loop = false;
        AudioSource_Music.Stop();
    }

    public void _StopSFX()
    {
        AudioSource_SFX.Stop();
    }

    public void ChangeSFXVolume(float value)
    {
        OverallVolume_SFX = value * 0.1f;
        AudioSource_SFX.volume = OverallVolume_SFX;
    }

    public void ChangeMusicVolume(float value)
    {
        OverallVolume_Music = value * 0.1f;
        AudioSource_Music.volume = OverallVolume_Music;
    }
}

[Serializable]
public class AudioFile
{
    public string Name;
    public AudioClip Clip => Clips[Random.Range(0, Clips.Length)];
    public AudioClip[] Clips;
    [Range(0, 1)]
    public float Volume;
    public AudioType Type;
}

public enum AudioType
{
    Music,
    SfX,
    Environment
}
