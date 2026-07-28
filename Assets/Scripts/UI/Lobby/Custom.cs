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

    private void OnEnable()
    {
        _birdAnimator.runtimeAnimatorController = _shopDatabase.GetShopItemById(2).Character.AnimController;
        if (_birdAnimator != null)
            _birdAnimator.Play("Shop");
        else
        {
            Debug.Log("Can not get Animator");
            return;
        }
        _birdName.text = _shopDatabase.GetShopItemById(2).Character.DisplayName;
        _birdPrice.text = _shopDatabase.GetShopItemById(2).Price.ToString();
    }
}
