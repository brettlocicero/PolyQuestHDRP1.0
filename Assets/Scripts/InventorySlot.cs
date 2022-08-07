using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Item item;
    public bool empty = true;

    public void PlaceInSlot (Item newItem)
    {
        if (!empty) return;

        item = newItem;
        empty = false;
    }
}
