using UnityEngine;

public enum SoundType
{
    Flap,
    TakePoint
}

[RequireComponent(typeof(AudioSource))]
public class SoundFXManager_Advance : MonoBehaviour
{
    public static SoundFXManager_Advance Instance;
    [SerializeField] private AudioClip[] soundList;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType soundType,float volume=1)
    {
        Instance.audioSource.PlayOneShot(Instance.soundList[(int)soundType], volume);
    }
}
