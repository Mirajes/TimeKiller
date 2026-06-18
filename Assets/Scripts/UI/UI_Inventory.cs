using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Canvas _mainCanvas;

    [SerializeField] private UI_DropWindow _groundContainer;
    //[SerializeField] private List<UI_InventoryCell> _groundCells;

    private void Start()
    {

        //for (int i = 0; i < _groundContainer.transform.childCount; i++)
        //{
            //_groundContainer.GetChild(i).GetComponent<UI_InventoryCell>().Init(_mainCanvas);
        //}
    }
}
