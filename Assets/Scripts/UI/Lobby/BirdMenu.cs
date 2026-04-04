
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;


public class BirdScriptMenu : MonoBehaviour
{
   
    
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _chatText;
    [SerializeField] private Image _imageChatText;
    private Rigidbody2D rb;
    private Image gameObjectImage;
    private List<string> ListChat = new List<string>();
    private bool didSpeak = false;
   




    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObjectImage = GetComponent<Image>();
        ListChat.Add("Hello my friend...");
        ListChat.Add("I don't want to die again..:(");
        ListChat.Add("Shhh...");
        ListChat.Add("I am really tired bro...");
        ListChat.Add("I want to have a vacation...");

    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Wall")){
            gameObjectImage.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.localPosition = new Vector2(0,0);
            rb.linearVelocity = Vector2.zero ;
            rb.angularVelocity = 0f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        Sequence s = DOTween.Sequence();

        s.AppendInterval(20f);
        s.OnComplete(() =>
        {
            gameObjectImage.enabled=true;
            _animator.enabled = true;
            _animator.Play("Perch", 0, 0f);
            Debug.Log("Play animation clip");
        });
    }

    


    private void HandleCollision()
    {
        int randN = UnityEngine.Random.Range(5, 10);
        int randP = UnityEngine.Random.Range(-10, -5);
        _animator.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = new Vector2(randP, randN);
        rb.angularVelocity = 50f;
        transform.rotation = Quaternion.Euler(0, 0, randN);
        

    }

    private void HandleSpeak()
    {
        if (!didSpeak)
        {
            int randN = UnityEngine.Random.Range(0, ListChat.Count);
            _imageChatText.gameObject.SetActive(true);
            _chatText.text = ListChat[randN];
            _chatText.maxVisibleCharacters = 0;
            DOTween.To(() => _chatText.maxVisibleCharacters,
                   x => _chatText.maxVisibleCharacters = x,
                   _chatText.text.Length, 1.5f)
               .SetEase(Ease.Linear);
            didSpeak = true;
            return;
        }
        _imageChatText.gameObject.SetActive(false);
        didSpeak = false;
    } 

    


}
