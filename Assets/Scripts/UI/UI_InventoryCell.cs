using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _parentCanvas; // how to move u to public available

    [SerializeField] private UI_InventorySlot _slot;

    [SerializeField] private bool _isInventory = false;
    [SerializeField] private ItemType _type = ItemType.None;

    public UI_InventorySlot Slot => _slot;

    public void Init(Canvas parentCanvas)
    {
        _parentCanvas = parentCanvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_slot == null) return;

        _slot.transform.SetParent(_parentCanvas.transform);
        _slot.HandleDrag(true);

        eventData.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_slot == null) return;

        _slot.transform.position = Vector2.Lerp(_slot.transform.position, eventData.position, 0.2f);
    }

    public void OnEndDrag(PointerEventData eventData) // PEREDELAT'
    {
        if (_slot == null) return;

        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UI_InventoryCell targetCell) && targetCell != this)
        {
            Debug.Log(targetCell);
            // 
            if (targetCell._type == _slot.Item.Type)
            {
                SwitchSlots(targetCell);
                UpdateMe();
            }
            else
            {
                ReturnSlotBack();
            }
        }
        else
        {
            Debug.Log(targetCell);
            ReturnSlotBack();
        }
    }

    private void ReturnSlotBack()
    {
        _slot.transform.SetParent(this.transform);
        _slot.HandleDrag(false);
    }

    private void SwitchSlots(UI_InventoryCell targetCell)
    {
        _slot.HandleDrag(false);

        UI_InventorySlot targetSlot = targetCell._slot;

        var tempSlot = targetCell._slot;
        targetCell._slot = _slot;
        _slot = tempSlot;

        targetCell._slot.transform.SetParent(targetCell.transform);
        _slot.transform.SetParent(this.transform);

        UpdateMe();
    }

    public void UpdateMe()
    {
        if (_slot.Item == null && !_isInventory)
            Destroy(this.gameObject);
    }
}
