using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<SceneLoadMusic> sceneMusicList;
    public Sound[] sounds;
    public static AudioManager instance;
    Sound activeMusic;
    Sound activeSFX;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = sound.output;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (SceneLoadMusic item in sceneMusicList)
        {
            if (scene.name == item.sceneName)
            {
                Play(item.musicName);
            }
        }
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void Play (string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        Debug.Log("playing " + name);
        sound.source.Play();
    }
    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            sound.source.Stop();
        }
    }
    public void Stop (string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        Debug.LogWarning("Stopping " + name);
        sound.source.Stop();
    }
    public void SetMusicVolume(float value)
    {
        //activeMusic.output.audioMixer.SetFloat("MusicVolume", AudioSetting.currentMusicValue-(value));
    }
}
[System.Serializable]
public class SceneLoadMusic
{
    public string sceneName;
    public string musicName;
}