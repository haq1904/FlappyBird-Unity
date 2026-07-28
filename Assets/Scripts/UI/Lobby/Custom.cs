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

    private int _currShopItemIndex;
    private int _selectedSkinId = 0;

    private void Start()
    {
        _leftBtn.onClick.AddListener(() => ChangeShopItem(-1));
        _rightBtn.onClick.AddListener(() => ChangeShopItem(1));
    }

    private void OnEnable()
    {
        Update_UI(_selectedSkinId);
        _currShopItemIndex = _shopDatabase.ItemList.FindIndex(x => x.Id == 0);
    }
    private void OnDisable()
    {
        _leftBtn.onClick.RemoveAllListeners();
        _rightBtn.onClick.RemoveAllListeners();
    }

    private void ChangeShopItem(int index)
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
            _birdPrice.text = currItem.Price.ToString();
        }
        else
        {
            Debug.Log("Can not get item list with id : " + skinId);
            return;
        }

    }




}
