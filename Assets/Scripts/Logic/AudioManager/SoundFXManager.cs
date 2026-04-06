using Unity.VisualScripting;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] AudioSource soundObject;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Sound Manager's instance has been created.");
        }
            
        else
        {
            Destroy(this);
        }
    }

    public void PlayAudioClip(AudioClip audioClip , Transform transform , float volume)
    {
        //Create sound object
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        //Assign sound object
        audioSource.clip = audioClip;

        //Get volume 
        audioSource.volume = volume;

        //Play audio
        audioSource.Play();

        //Get audio's lenght
        float clipLenght = audioSource.clip.length;

        //Destroy sound object
        Destroy(audioSource.gameObject, clipLenght);

    }

    public void PlayRandomAudioClip(AudioClip[] audioClip, Transform transform, float volume)
    {
        //Create sound object
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        int randNum = UnityEngine.Random.Range(0,audioClip.Length);
        Debug.Log(randNum);

        //Assign sound object
        audioSource.clip = audioClip[randNum];

        //Get volume 
        audioSource.volume = volume;

        //Play audio
        audioSource.Play();

        //Get audio's lenght
        float clipLenght = audioSource.clip.length;

        //Destroy sound object
        Destroy(audioSource.gameObject, clipLenght);

    }

}
