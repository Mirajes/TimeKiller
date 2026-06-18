using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PromotionOffer : MonoBehaviour
{
    [SerializeField] private SO_Item _item;

    [SerializeField] private Image _iconBig;
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _costField;
    [SerializeField] private Button _buyButton;

    [SerializeField] private TMP_Text _descriptionField;
    [SerializeField] private int _countToBuy;

    [SerializeField] private LayoutElement _layoutElement;
    public LayoutElement LayoutElement => _layoutElement;

    public void UpdateOffer(int maxItemBuy)
    {
        _countToBuy = Random.Range(0, maxItemBuy);
    }

    public void UpdateWindow() 
    {

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

    }
}