using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    public AudioMixer audioMixer;
    float minAudioVolume = -60f;
    float maxAudioVolume = 0f;
    public static float currentMusicValue;
    public static float currentSFXValue;
    void Awake()
    {
        SetAudioVolume(musicSlider,SoundType.MusicVol,currentMusicValue);
        SetAudioVolume(sfxSlider,SoundType.SFXVol, currentSFXValue);
    }
    public void SetMusicVolume()
    {
        float value = musicSlider.value;
        currentMusicValue = AudioVolume(value);
        audioMixer.SetFloat("MusicVol", AudioVolume(value));
    }
    public void SetSFXVolume()
    {
        float value = sfxSlider.value;
        currentSFXValue = AudioVolume(value);
        audioMixer.SetFloat("SFXVol", AudioVolume(value));
    }
    public void IncreaseVolume(string soundType)
    {
        switch (soundType)
        {
            case "Music":
                musicSlider.value += 0.05f;
                SetMusicVolume();
                break;
            case "SFX":
                sfxSlider.value += 0.05f;
                SetSFXVolume();
                break;
        }
    }
    public void DecreaseVolume(string soundType)
    {
        switch (soundType)
        {
            case "Music":
                musicSlider.value -= 0.05f;
                SetMusicVolume();
                break;
            case "SFX":
                sfxSlider.value -= 0.05f;
                SetSFXVolume();
                break;
        }
    }
    private void SetAudioVolume(Slider audioSlider, SoundType soundType, float result)
    {
        audioMixer.SetFloat(soundType.ToString(), result);
        float sliderValue = (1/AbsoluteMinimum * result) + 1;
        audioSlider.value = sliderValue;
    }
    private float AudioVolume(float value)
    {
        float volume = (AbsoluteMinimum*value) - AbsoluteMinimum;
        return Mathf.Clamp(volume,minAudioVolume,maxAudioVolume);
    }
    [System.Serializable] public enum SoundType{
        MusicVol, SFXVol, MusicPitch, SFXPitch
    }
    float AbsoluteMinimum => Mathf.Abs(minAudioVolume);
}
