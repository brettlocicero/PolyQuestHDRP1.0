using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool empty = true;
    [SerializeField] Image slotHighlight;
    public Item item;

    public void OnPointerEnter (PointerEventData eventData) 
    {
        slotHighlight.color = Color.white;
    }

    public void OnPointerExit (PointerEventData eventData) 
    {
        slotHighlight.color = Color.black;
    }
}
