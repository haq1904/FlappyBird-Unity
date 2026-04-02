using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Xml.Serialization;

public class BirdScriptMenu : MonoBehaviour
{
   
    
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _chatText;
    [SerializeField] private Image _imageChatText;
    private Rigidbody2D rb;
     
    private List<string> ListChat = new List<string>();
    private bool didSpeak = false;
   




    private void OnEnable()
    {
        

    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ListChat.Add("F*ck u!");
        ListChat.Add("Do i die again:(");
        ListChat.Add("Shhh...");
        ListChat.Add("You suck");
        ListChat.Add("Let me relax...");


    }
    void Update()
    {
        
        if (transform.localPosition.y < -1000)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.localPosition = new Vector2(-348f, 1443f);
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
