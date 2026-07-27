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
    [SerializeField] private GameObject _bird;
    [SerializeField] private TextMeshProUGUI _birdName;
    [SerializeField] private TextMeshProUGUI _birdPrice;

    private void OnEnable()
    {
        Animator animator = _bird.GetComponent<Animator>();
        animator.runtimeAnimatorController = _shopDatabase.GetShopItemById(0).Character.AnimController;
        if (animator != null)
        {
            animator.Play("Flying");
        }
        _birdName.text = _shopDatabase.GetShopItemById(0).Character.DisplayName;
        _birdPrice.text = _shopDatabase.GetShopItemById(0).Price.ToString();
    }
}
