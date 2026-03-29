using UnityEngine;
using UnityEngine.Audio;

public class SoundAudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        Debug.Log($"Slider's value : {volume}");
        //audioMixer.SetFloat("masterVolume", volume);
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
        Debug.Log($"Master's volume changed at {Mathf.Log10(volume) * 20f} db.");
    }

    public void SetSoundFXVolume(float volume)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(volume) * 20f);
        Debug.Log($"SoundFx's volume changed at {Mathf.Log10(volume) * 20f} db.");
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
        Debug.Log($"Music's volume changed at {Mathf.Log10(volume) * 20f} db.");
    }
}
