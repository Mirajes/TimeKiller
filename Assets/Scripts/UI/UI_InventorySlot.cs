using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;

    [SerializeField] private SO_Item _item;
    [SerializeField] private RectTransform _rect;
    [SerializeField] private ItemType _type;

    [SerializeField] private CanvasGroup _canvasGroup;

    public SO_Item Item => _item;
    public RectTransform Rect => _rect;
    public ItemType Type => _type;

    public void UpdateSlot()
    {
        if (_item == null) return;

        _icon.sprite = _item.SmallIcon;
    }

    public void HandleDrag(bool isDragging)
    {
        if (isDragging)
        {
            //_canvasGroup.blocksRaycasts = false; // useless
            _canvasGroup.alpha = 0.8f;
        }
        else
        {
            //_canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
        }
    }

    private void Start()
    {
        UpdateSlot();
    }
}
