using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using Unity.XR.OpenVR;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private GameObject[] birds;
    public float duration=1f;
    private Ease moveEase = Ease.InOutBack;
    private List<Vector3> resetBirdsPos;

    private void Awake()
    {
        resetBirdsPos = new List<Vector3>();
        for(int i = 0; i < birds.Length; i++)
        {
            resetBirdsPos.Add(birds[i].transform.localPosition);
        }
    }
    public void OnEnable()
    {
        StartGame();
        EasyModeUIController.OnStartGame += StartGame;

    }

    private void OnDisable()
    {
        EasyModeUIController.OnStartGame -= StartGame;
    }

    private void StartGame()
    {
        ResetBirds();
        var sequence = DOTween.Sequence();
        sequence.Append(panel.DOAnchorPos(new Vector2(0, 307), duration).SetEase(moveEase));


        for (int i = 0; i < birds.Length; i++)
        {
            int index = i;
            sequence.AppendCallback(() =>
            {
                float randX = UnityEngine.Random.Range(-2,2);
                float randY = UnityEngine.Random.Range(2, 4);
                Rigidbody2D rb = birds[index].GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(new Vector2(randX, randY), ForceMode2D.Impulse);
                rb.angularVelocity = 200f;
            });
            sequence.AppendInterval(0.8f);
        }

        sequence.Append(panel.DOAnchorPos(new Vector2(0, 724), duration).SetEase(moveEase));

        sequence.SetLink(gameObject,LinkBehaviour.KillOnDisable);

        sequence.AppendCallback(()=> {
            gameObject.SetActive(false);
         });

    }

    private void ResetBirds()
    {
       for(int i = 0; i< birds.Length; i++)
        {
            birds[i].transform.localPosition = resetBirdsPos[i];
            Rigidbody2D rb = birds[i].GetComponent<Rigidbody2D>();
            birds[i].transform.eulerAngles= Vector3.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
