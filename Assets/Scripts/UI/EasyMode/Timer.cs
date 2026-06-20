using Codice.Client.Common.GameUI;
using DG.Tweening;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent OnTimerDoneEvent; 

    [Header("Game objects")]
    [SerializeField] private RectTransform panel;
    [SerializeField] private GameObject[] birds;
    [SerializeField] private CanvasGroup canvasGroupTimer;

    [Header("Fields")]
    [SerializeField] private float duration=1f;
    [SerializeField] private float timeToCountdown = 1;

    [Header("Shake")]
    [SerializeField] float _durationForShake = 1;
    [SerializeField] Vector2 streght = new Vector2(5,5);
    [SerializeField] float vibrato = 10;
    [SerializeField] int randomness = 90;


    private Ease moveEase = Ease.InOutBack;
    private List<Vector3> resetBirdsPos;
    private Sequence mainSequence;

    private void Awake()
    {
        resetBirdsPos = new List<Vector3>();
        for(int i = 0; i < birds.Length; i++)
        {
            resetBirdsPos.Add(birds[i].transform.localPosition);
        }
    }
    
    public void TurnOn()
    {
        gameObject.SetActive(true);
        Play();
    }

    public void TurnOff()
    {
        panel.anchoredPosition = new Vector3(0, 200, 0);
        mainSequence.Kill();
        gameObject.SetActive(false);
    }


    private void Play()
    {
        canvasGroupTimer.alpha = 1;
        ResetBirds();
        mainSequence = DOTween.Sequence();
        mainSequence.Append(panel.DOAnchorPos(new Vector2(0, -248), duration).SetEase(moveEase));


        for (int i = 0; i < birds.Length; i++)
        {
            int index = i;
            mainSequence.AppendCallback(() =>
            {
                float randX = UnityEngine.Random.Range(-2, 2);
                float randY = UnityEngine.Random.Range(2, 4);
                Rigidbody2D rb = birds[index].GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(new Vector2(randX, randY), ForceMode2D.Impulse);
                rb.angularVelocity = 200f;
                panel.DOShakeAnchorPos(_durationForShake, vibrato, randomness, snapping : false, fadeOut : true);

            });
            mainSequence.AppendInterval(timeToCountdown);
        }
        mainSequence.AppendCallback(() => {
            OnTimerDoneEvent?.Invoke();
        }
        );
        mainSequence.AppendInterval(0.8f);

        mainSequence.Append(panel.DOAnchorPos(new Vector2(0, 184), duration).SetEase(moveEase));

        mainSequence.AppendCallback(() => {
            canvasGroupTimer.alpha = 0;
            TurnOff();
            });
    }

    private void ResetBirds()
    {
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i].transform.localPosition = resetBirdsPos[i];
            Rigidbody2D rb = birds[i].GetComponent<Rigidbody2D>();
            birds[i].transform.eulerAngles = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
