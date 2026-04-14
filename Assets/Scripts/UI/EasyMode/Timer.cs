using DG.Tweening;
using System.Collections.Generic;
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

    public void StartCountDown()
    {
        canvasGroupTimer.alpha = 1;
        ResetBirds();
        var sequence = DOTween.Sequence();
        sequence.Append(panel.DOAnchorPos(new Vector2(0, -248), duration).SetEase(moveEase));


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
        sequence.AppendCallback(() => {
            Debug.Log("Timer notified : OnTimerDone event is raised. ");
            OnTimerDoneEvent?.Invoke();          
            }
        );

        sequence.Append(panel.DOAnchorPos(new Vector2(0, 184), duration).SetEase(moveEase));

        sequence.SetLink(gameObject,LinkBehaviour.KillOnDisable);

        sequence.AppendCallback(()=>canvasGroupTimer.alpha = 0);

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
