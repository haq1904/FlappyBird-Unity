using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class EasyModeUIController : MonoBehaviour
{
    [SerializeField] private Canvas _Timer;
    [SerializeField] private GameObject timer;
    [SerializeField] private float duration = 2;
    private Ease moveEase = Ease.OutQuart;
    private RectTransform timerRect;
    private Timer timerScript;
    public void OnEnable()
    {
        timerRect = timer.GetComponent<RectTransform>();
        timerScript = timer.GetComponent<Timer>();
        EasyModeManager.OnStartGame += StartGame;
    }

    public void OnDisable()
    {
        EasyModeManager.OnStartGame -= StartGame;
    }

    public void StartGame()
    {

        timerScript.enabled = true;
        timerRect.DOAnchorPos(new Vector2(0, 307), duration).SetEase(moveEase).SetLink(gameObject);
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(3.5f);
        sequence.AppendCallback(() =>
        {
            timerScript.enabled = false;
            timerRect.DOAnchorPos(new Vector2(0, 724), duration).SetEase(moveEase).SetLink(gameObject);
        });
        sequence.SetLink(gameObject);


    }
}
