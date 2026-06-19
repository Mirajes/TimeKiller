using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PromotionOffer : MonoBehaviour
{
    [Header("Content")]
    [SerializeField] private Image _iconBig;
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _costField;
    [SerializeField] private TMP_Text _descriptionField;

    [SerializeField] private SO_Item _item;
    [SerializeField] private Button _buyButton;

    //[SerializeField] private int _countToBuy;

    [SerializeField] private LayoutElement _layoutElement;
    public LayoutElement LayoutElement => _layoutElement;

    //public void UpdateOffer(int maxItemBuy)
    //{
        //_countToBuy = Random.Range(0, maxItemBuy);
    //}

    public void UpdateWindow() 
    {
        if (_item != null)
        {
            _iconBig.sprite = _item.SmallIcon;
            _nameField.text = "Name: " + _item.Name;
            _costField.text = "Ad Cost: " + _item.AdCost.ToString();
            _descriptionField.text = "About: " + _item.Description;
        }
    }

    private void Start()
    {
        _buyButton.onClick.AddListener(HandleBuy);
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveAllListeners();
    }

    private void HandleBuy()
    {
        GameManager.BuyItem?.Invoke(_item);
    }
}