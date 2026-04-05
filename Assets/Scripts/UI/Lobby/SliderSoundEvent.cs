using System;
using UnityEngine;


public class SliderSoundEvent : MonoBehaviour
{
    public enum SliderType{
        Master,
        SoundFx,
        Music
    }

    [SerializeField] private SliderType sliderType;

    public static Action<SliderType,float> OnChangeSliderValue;

    public void ChangeSlideValue(float value){
        OnChangeSliderValue?.Invoke(sliderType,value);
    }
    
}
