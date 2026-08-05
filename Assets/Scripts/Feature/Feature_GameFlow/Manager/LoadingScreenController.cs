using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreenController : MonoBehaviour
{
    public static LoadingScreenController Instance;

    [Header("Fields")]
    [SerializeField] private GameObject _scrollDown;
    [SerializeField] private GameObject _scrollUp;
    [SerializeField] private GameObject _botMarker;

    [Header("Move Fields")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    private Vector2 _resetPos;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _resetPos = _scrollDown.GetComponent<RectTransform>().anchoredPosition;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleLoadScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleLoadScene;
        _scrollDown.transform.DOKill(true);
        _scrollUp.transform.DOKill(true);
    }

    //use this function for receiving event from scene manager(belong to unity)
    private void HandleLoadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        _scrollDown.SetActive(false);
        _scrollDown.GetComponent<RectTransform>().anchoredPosition = _resetPos;
        LoadingScrollUp();
    }

    public void LoadingBoth()
    {
        _scrollDown.SetActive(true);
        _scrollDown.transform.DOMoveY(_botMarker.transform.position.y, _moveDuration)
        .SetEase(_moveEase)
        .SetLink(gameObject)
        .OnComplete(() =>
        {
            _scrollUp.SetActive(true);
            _scrollDown.SetActive(false);
            _scrollUp.transform.DOMoveY(_botMarker.transform.position.y, _moveDuration)
            .SetEase(_moveEase)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                _scrollUp.SetActive(false);
                _scrollDown.GetComponent<RectTransform>().anchoredPosition = _resetPos;
                _scrollUp.GetComponent<RectTransform>().anchoredPosition = _resetPos;
            });
        });
    }

    public void LoadingScrollDown(bool isAutoOff)
    {
        Scroll(_scrollDown, isAutoOff);

    }

    public void LoadingScrollUp()
    {
        Scroll(_scrollUp);

    }

    private void Scroll(GameObject scrollType, bool isAutoOff = true)
    {
        scrollType.SetActive(true);
        scrollType.transform.DOMoveY(_botMarker.transform.position.y, _moveDuration)
        .SetEase(_moveEase)
        .SetLink(gameObject)
        .OnComplete(() =>
        {
            scrollType.SetActive(!isAutoOff);
            if (isAutoOff)
                scrollType.GetComponent<RectTransform>().anchoredPosition = _resetPos;

        });
    }

    public float GetMinimumMoveDuration()
    {
        return _moveDuration;
    }




}
