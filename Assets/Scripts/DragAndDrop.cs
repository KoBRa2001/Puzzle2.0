using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private DragController _dragController;
    [SerializeField] private Image _image; 
    [SerializeField] private Sprite _icon;


    public event Action OnDestroyEvent = null;

    private CanvasGroup canvasGroup;
    private Camera _camera;

    private Transform startParent;
    private Vector3 startPosition;

    public Sprite Icon => _icon;

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

    public void Setup(DragController dragController)
    {
        _dragController = dragController;
    }

    public void OnBeginDrag(PointerEventData touch)
    {
        _dragController.SetItem(this);
        
        canvasGroup.blocksRaycasts = false;        
    }

    internal void SetRand()
    {
        var rand = UnityEngine.Random.Range(1, 4);
        var type = (DiamandType)rand;
        var path = "Icons/" + type.ToString();
        
        _icon = Resources.Load<Sprite>(path);
        _image.sprite = _icon;
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

    private void OnDestroy()
    {
        OnDestroyEvent?.Invoke();
    }
}
