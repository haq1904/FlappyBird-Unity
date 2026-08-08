using System;
using UnityEngine;

public enum SoundType
{
    Flap,
    TakePoint,
    TakeCoin,
    Click
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundFXManager_Advance : MonoBehaviour
{
    public static SoundFXManager_Advance Instance;
    [SerializeField] private SoundList[] soundList;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        string[] name = Enum.GetNames(typeof(SoundType));

        if (soundList == null)
        {
            soundList = new SoundList[name.Length];
        }
        else if (soundList.Length != name.Length)
        {
            Array.Resize(ref soundList, name.Length);
        }

        for (int i = 0; i < name.Length; i++)
        {
            soundList[i].name = name[i];
        }
    }
#endif

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType soundType)
    {
        float volume = 1;
        AudioClip[] clips = Instance.soundList[(int)soundType].Sounds;
        AudioClip randClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        Instance.audioSource.PlayOneShot(randClip, volume);
    }
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
