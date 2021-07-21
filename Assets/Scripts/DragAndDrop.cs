using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private DragController _dragController; 

    private CanvasGroup canvasGroup;
    private Camera _camera;

    private Transform startParent;
    private Vector3 startPosition;
    private Vector3 offset;
    
    private void Awake()
    {
        _camera = Camera.main;
        
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        startParent = transform.parent;
        startPosition = transform.position;        
    }

    public void OnBeginDrag(PointerEventData touch)
    {
        _dragController.SetItem(this);
        
        canvasGroup.blocksRaycasts = false;        
    }

    public void OnDrag(PointerEventData touch)
    {
        transform.position = _camera.ScreenPointToRay(touch.position).origin;
    }    

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_dragController.SelectedItem == this)
            Reset();

        canvasGroup.blocksRaycasts = true;
    }

    public void Reset()
    {
        transform.SetParent(startParent);
        transform.position = startPosition;
    }
}
