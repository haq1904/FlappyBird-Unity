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
            Debug.Log("Dont have enough coins");
        }
    }





}
