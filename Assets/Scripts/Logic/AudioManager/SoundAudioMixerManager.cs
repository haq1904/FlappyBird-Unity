using System;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.Audio;

public class SoundAudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    //private void OnEnable(){
    //    SliderSoundEvent.OnChangeSliderValue += ChangeVolumeValue;
    //}

    //private void OnDisable(){
    //    SliderSoundEvent.OnChangeSliderValue -= ChangeVolumeValue;
    //}

    //private void ChangeVolumeValue(SliderSoundEvent.SliderType sliderType, float  volume)
    //{
    //   if(sliderType== SliderSoundEvent.SliderType.Master)
    //    {
    //        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
    //    }
    //    else if(sliderType== SliderSoundEvent.SliderType.SoundFx)
    //    {
    //        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(volume) * 20f);
    //    }
    //    else if(sliderType== SliderSoundEvent.SliderType.Music)
    //    {
    //        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
    //    }
    //}

    
}
