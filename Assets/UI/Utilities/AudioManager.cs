using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
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
        // Debug.Log($"playing {sound.clip} name {sound.name} output {sound.output.name}");
        if (sound.output.name == "SFX")
        {
            // if (activeSFX != null)
            // {
            //     Stop(activeSFX.name);
            // }
            activeSFX = sound;
        }
        if (sound.output.name == "Music")
        {
            if (activeMusic != null)
            {
                Stop(activeMusic.name);
            }
            activeMusic = sound;
        }
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
        activeMusic.output.audioMixer.SetFloat("MusicVolume", AudioSetting.currentMusicValue-(value));
    }
}