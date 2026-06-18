using UnityEngine;

public class UI_DropWindow : MonoBehaviour
{
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private RectTransform _dropContainer;
    [SerializeField] private UI_InventoryCell _cellPrefab;
    [SerializeField] private UI_InventorySlot _slotPrefab;

    public void AddSlot(UI_InventorySlot slot)
    {
        var newCell = Instantiate(_cellPrefab, _dropContainer.transform);
        var newSlot = Instantiate(_slotPrefab, newCell.transform);

        newCell.Init(_parentCanvas, newSlot);

        newCell.Slot.Init(slot.Item);
    }

    public void AddSlot(SO_Item item)
    {
        var newCell = Instantiate(_cellPrefab, _dropContainer.transform);
        var newSlot = Instantiate(_slotPrefab, newCell.transform);

        newSlot.Init(item);
        newCell.Init(_parentCanvas, newSlot);
    }
}