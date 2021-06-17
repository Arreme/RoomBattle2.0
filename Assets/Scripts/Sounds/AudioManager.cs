using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioFile[] AudioFiles;
    public AudioFile[] MusicFiles => AudioFiles.Where(x => x.Type == AudioType.Music).ToArray();
    public AudioSource AudioSource_Music;
    public AudioSource AudioSource_SFX;

    [Range(0, 1)]
    public float OverallVolume_SFX;
    [Range(0, 1)]
    public float OverallVolume_Music;

    public AudioMixer AudioMixer;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    public static void PlayMusic(string name)
    {
        Instance._PlayMusic(name);
    }

    public void _PlayMusic(string soundName)
    {
        var file = GetFileByName(soundName);

        if (file != null)
        {
            var clip = file.Clip;
            AudioSource_Music.volume = file.Volume * OverallVolume_Music;
            if (clip != null)
            {
                AudioSource_Music.clip = clip;
                AudioSource_Music.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                    AudioMixer.FindMatchingGroups(name)[0] :
                    AudioMixer.FindMatchingGroups("Music")[0];
                AudioSource_Music.Play();
            }
        }
        else
        {
            Debug.LogError("Trying to play sound that does no exist, merluzo! " + soundName);
        }
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
                    AudioMixer.FindMatchingGroups("Sfx")[0];
            AudioSource_SFX.volume = file.Volume = OverallVolume_SFX;
            AudioSource_SFX.clip = clip;
            AudioSource_SFX.Play();
        }
        else
        {
            Debug.LogError("Trying to play sound that does no exist, merluzo! " + soundName);
        }
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
