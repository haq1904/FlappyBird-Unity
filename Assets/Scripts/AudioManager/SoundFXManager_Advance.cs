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

    private int _coinComboIndex = 0;
    private float _lastCoinTime = 0f;
    private float _comboResetTime = 1.5f; // Thời gian tối đa giữa 2 lần ăn xu để giữ chuỗi

    public static void PlaySound(SoundType soundType)
    {
        float volume = 1;
        AudioClip[] clips = Instance.soundList[(int)soundType].Sounds;
        AudioClip clipToPlay;

        // Nếu là âm thanh ăn xu, áp dụng logic phát theo thứ tự
        if (soundType == SoundType.TakeCoin && clips.Length > 0)
        {
            // Kiểm tra xem đã quá thời gian giữ chuỗi combo chưa
            if (Time.time - Instance._lastCoinTime > Instance._comboResetTime)
            {
                Instance._coinComboIndex = 0; // Reset lại từ đầu
            }

            // Lấy clip theo thứ tự
            clipToPlay = clips[Instance._coinComboIndex];

            // Tăng thứ tự lên 1. Nếu vượt quá số clip đang có thì giữ nguyên ở clip cuối
            Instance._coinComboIndex++;
            if (Instance._coinComboIndex >= clips.Length)
            {
                Instance._coinComboIndex = clips.Length - 1; // Hoặc để (clips.Length - 1) nếu muốn kêu tiếng cao nhất hoài
            }

            // Cập nhật lại thời gian vừa ăn xu
            Instance._lastCoinTime = Time.time;
        }
        else // Các âm thanh khác thì random như cũ
        {
            clipToPlay = clips[UnityEngine.Random.Range(0, clips.Length)];
        }

        Instance.audioSource.PlayOneShot(clipToPlay, volume);
    }

    // Nếu ông muốn ép Reset thủ công (khi chim chết, hay qua màn) thì gọi hàm này
    public static void ResetCoinCombo()
    {
        if (Instance != null)
        {
            Instance._coinComboIndex = 0;
        }
    }
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
