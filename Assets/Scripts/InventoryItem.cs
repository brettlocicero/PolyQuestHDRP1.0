using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    InventoryManager im;
    void Start () => im = InventoryManager.instance;

    public Item mainItem;
    public Transform oldSlot;
    [SerializeField] Image img;
    [SerializeField] Image rarityImg;

    public void OnPointerEnter (PointerEventData eventData) 
    {
        im.UpdateItemInfoPanel(mainItem);
    }

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

    public void UpdateItemDisplay (Item item) 
    {
        mainItem = item;
        img.sprite = item.sprite;
        rarityImg.color = InventoryManager.instance.rarityColors[item.rarity];
    }
}
