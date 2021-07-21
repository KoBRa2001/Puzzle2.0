using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour//, IDropHandler
{   
    [SerializeField] private DragController _dragController;
    [SerializeField] private ItemInfo diamond;
    private bool isEmpty = true;

    private void OnMouseOver()
    {        

        if (isEmpty)
        {
            if (Input.GetMouseButtonUp(0) && _dragController.HasSelectedItem())
            {
                GameObject item = _dragController.TakeSelectedItem().gameObject;
                if (item.GetComponent<ItemInfo>())
                {
                    diamond.type = item.GetComponent<ItemInfo>().type;
                    Destroy(item);

                    diamond.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                    Instantiate(diamond, transform);
                    isEmpty = false;
                }                
            }
        }
    }
}
