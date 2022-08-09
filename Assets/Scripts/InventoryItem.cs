using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    InventoryManager im;
    void Start () => im = InventoryManager.instance;

    public Item item;
    public Transform oldSlot;

    public void OnBeginDrag (PointerEventData eventData) 
    {
        oldSlot = transform.parent;
        transform.SetParent(im.itemPanel);
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag (PointerEventData eventData) 
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag (PointerEventData eventData) 
    {
        if (eventData.pointerEnter) 
        {
            if (eventData.pointerEnter.TryGetComponent(out InventorySlot slot)) 
            {
                print("slot");
                transform.SetParent(slot.transform);
                transform.localPosition = Vector3.zero;
                oldSlot = slot.transform;
            }

            else 
            {
                transform.SetParent(oldSlot);
                transform.localPosition = Vector3.zero;
            }
        }

        else 
        {
            transform.SetParent(oldSlot);
            transform.localPosition = Vector3.zero;
        }
        
        GetComponent<Image>().raycastTarget = true;
    }
}
