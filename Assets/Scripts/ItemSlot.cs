using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour//, IDropHandler
{   
    [SerializeField] private DiamandItem _diamondPrefab;

    private DragController _dragController;
    private GridController _gridController;

    private DiamandItem _itemInSlot;

    private bool isEmpty => _itemInSlot == null;

    public int x; 
    public int y; 

    public void Setup(DragController dragController, GridController gridController)
    {
        _dragController = dragController;
        _gridController = gridController;
    }

    public void ClearSlot()
    {
        if (_itemInSlot == null)
            return;

        Destroy(_itemInSlot.gameObject);
        _itemInSlot = null;
    }

    private void OnMouseOver()
    {
        if (!isEmpty)
            return;

        if (Input.GetMouseButtonUp(0) && _dragController.HasSelectedItem())
        {
            var selectedItem = _dragController.TakeSelectedItem();

            _itemInSlot = Instantiate(_diamondPrefab, transform);
            _itemInSlot.SetSprite(selectedItem.Icon);            
            Destroy(selectedItem.gameObject);
            
            _gridController.SetCell(x, y);
        }

    }

    
}
