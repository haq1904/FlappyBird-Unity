using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Custom : MonoBehaviour
{
    [Header("Skin database")]
    [SerializeField] private ShopDatabaseService _shopDatabase;

    [Header("Button")]
    [SerializeField] private Button _useBtn;
    [SerializeField] private Button _buyBtn;
    [SerializeField] private Button _leftBtn;
    [SerializeField] private Button _rightBtn;

    [Header("Bird container")]
    [SerializeField] private Animator _birdAnimator;
    [SerializeField] private TextMeshProUGUI _birdName;
    [SerializeField] private TextMeshProUGUI _birdPrice;

    [Header("Fields")]
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private RectTransform _messagePanel;
    [SerializeField] private float _messagePanelDuration;
    [SerializeField] private Ease _messagePanelMoveEase;
    [SerializeField] private TextMeshProUGUI _messageText;

    [Header("Buy button shake fields")]
    [SerializeField] private float _buyButtonShakeDuration = 1;
    [SerializeField] private Vector3 _buyButtonShakeStrength;
    [SerializeField] private int _buyButtonShakeVibrato = 10;
    [SerializeField] private float _buyButtonShakeRandomness = 90;

    private int _currShopItemIndex;
    private DataManagerService _dataManager;

    private void Awake()
    {
        _dataManager = FindAnyObjectByType<DataManagerService>();
        //_dataManager.AddCoins(100);
    }

    private void Start()
    {
        _leftBtn.onClick.AddListener(() => OnClickChangeItem(-1));
        _rightBtn.onClick.AddListener(() => OnClickChangeItem(1));
        _buyBtn.onClick.AddListener(() => OnClickBuy());
        _useBtn.onClick.AddListener(() => OnClickUse());
    }

    private void OnEnable()
    {
        if (_dataManager == null)
        {
            Debug.Log("Can not get Data manager");
            return;
        }
        Update_UI(_dataManager.GetSelectedSkin());
        _currShopItemIndex = _shopDatabase.ItemList.FindIndex(x => x.Id == _dataManager.GetSelectedSkin());
    }
    private void OnDisable()
    {
        _leftBtn.onClick.RemoveAllListeners();
        _rightBtn.onClick.RemoveAllListeners();
        _buyBtn.onClick.RemoveAllListeners();
        _useBtn.onClick.RemoveAllListeners();
    }

    private void OnClickChangeItem(int index)
    {
        _currShopItemIndex += index;
        if (_currShopItemIndex < 0)
            _currShopItemIndex = _shopDatabase.ItemList.Count - 1;
        else if (_currShopItemIndex >= _shopDatabase.ItemList.Count)
            _currShopItemIndex = 0;
        Update_UI(_shopDatabase.ItemList[_currShopItemIndex].Id);
    }

    private void Update_UI(int skinId)
    {
        int itemIndexToUpdate = _shopDatabase.ItemList.FindIndex(x => x.Id == skinId);
        if (itemIndexToUpdate >= 0)
        {
            _currShopItemIndex = itemIndexToUpdate;
            ShopItemService currItem = _shopDatabase.ItemList[itemIndexToUpdate];
            _birdAnimator.runtimeAnimatorController = currItem.Character.AnimController;
            _birdAnimator.Play("Shop", -1, 0f);
            _birdName.text = currItem.Character.DisplayName;
            if (_dataManager.IsSkinUnlocked(currItem.Id))
            {
                _buyPanel.SetActive(false);
                _useBtn.gameObject.SetActive(true);
                if (currItem.Id == _dataManager.GetSelectedSkin())
                {
                    _useBtn.interactable = false;
                }
                else
                {
                    _useBtn.interactable = true;
                }
            }
            else
            {
                _buyPanel.SetActive(true);
                _useBtn.gameObject.SetActive(false);
                _birdPrice.text = currItem.Price.ToString();
            }

        }
        else
        {
            Debug.Log("Can not get item list with id : " + skinId);
            return;
        }
    }

    private void OnClickBuy()
    {
        if (_dataManager == null)
        {
            Debug.Log("Can not get Data Manager!");
            return;
        }
        ShopItemService currItem = _shopDatabase.ItemList[_currShopItemIndex];
        if (_dataManager.SpendCoins(currItem.Price))
        {
            _dataManager.UnlockSkin(currItem.Id);
            Update_UI(currItem.Id);
        }
        else
        {
            _buyBtn.interactable = false;
            LoadMessage("Don't have enough coin");
            _buyBtn.gameObject.transform.DOShakePosition(_buyButtonShakeDuration, _buyButtonShakeStrength, _buyButtonShakeVibrato, _buyButtonShakeRandomness)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                _buyBtn.interactable = true;
            });
        }
    }

    private void OnClickUse()
    {
        _dataManager.SetSelectedSkin(_shopDatabase.ItemList[_currShopItemIndex].Id);
        Update_UI(_shopDatabase.ItemList[_currShopItemIndex].Id);
    }

    private void LoadMessage(string content)
    {
        _messagePanel.gameObject.SetActive(true);
        _messagePanel.DOKill();
        _messageText.DOKill();
        _messagePanel.anchoredPosition = new Vector3(0, -80, 0);
        _messageText.text = content;
        _messageText.DOFade(1f, 0).SetLink(gameObject);
        _messagePanel.DOAnchorPosY(170, _messagePanelDuration).SetEase(_messagePanelMoveEase).SetLink(gameObject);
        _messageText.DOFade(0f, _messagePanelDuration)
        .SetEase(Ease.Linear)
        .SetLink(gameObject)
        .OnComplete(() =>
        {
            _messagePanel.gameObject.SetActive(false);
        });
    }





}
