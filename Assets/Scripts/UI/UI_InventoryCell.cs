using UnityEngine;
using UnityEngine.EventSystems;

// remake pls
public class UI_InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("For Move")]
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private UI_InventorySlot _slot;

    [Header("For Inventory")]
    [SerializeField] private bool _isInventory = false;
    [SerializeField] private ItemType _cellType = ItemType.None;

    public UI_InventorySlot Slot => _slot;

    public void Init(Canvas parentCanvas, UI_InventorySlot slot)
    {
        _parentCanvas = parentCanvas;
        _slot = slot;
    }

    private void SwitchSlots(UI_InventoryCell targetCell) 
    {
        if (targetCell._slot != null)
        {
            _slot.HandleDrag(false);

            UI_InventorySlot tempSlot = targetCell._slot;
            targetCell._slot = _slot;
            _slot = tempSlot;

            targetCell._slot.transform.SetParent(targetCell.transform);
            _slot.transform.SetParent(this.transform);
        }
        else
        {
            _slot.HandleDrag(false);
            targetCell._slot = _slot;
            targetCell._slot.transform.SetParent(targetCell.transform);

            _slot = null;

            if (!_isInventory)
                Destroy(this.gameObject);
        }
    }

    private void ReturnSlot()
    {
        _slot.transform.SetParent(this.transform);
        _slot.HandleDrag(false);
    }

    private void CreateSlotInDrop(UI_DropWindow dropWindow)
    {
        dropWindow.AddSlot(_slot);
        Destroy(_slot.gameObject);
        _slot = null;

        if (!_isInventory)
            Destroy(this.gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_slot == null) return; // need to check in different way

        AudioManager.Instance.PlaySFX("take");

        _slot.transform.SetParent(_parentCanvas.transform);
        _slot.HandleDrag(true);

        eventData.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_slot == null) return;

        _slot.transform.position = Vector2.Lerp(_slot.transform.position, eventData.position, 0.2f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_slot == null) return;

        AudioManager.Instance.PlaySFX("drop");

        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UI_InventoryCell targetCell))
        {
            if (targetCell._isInventory && targetCell._cellType == _slot.Item.Type)
            {
                SwitchSlots(targetCell);
            }
            else if (!targetCell._isInventory)
            {
                SwitchSlots(targetCell);
            }
            else
            {
                ReturnSlot();
                UIManager.Notify?.Invoke($"That point bad. {_slot.Item.Type} != {targetCell._cellType}", NotificationType.Warn);
            }
        }
        else
        {
            ReturnSlot();
        }

        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out UI_DropWindow dropWindow))
        {
            CreateSlotInDrop(dropWindow);
        }
    }
}

/*

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

 */