using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] slots;

    public void AddItem (Item item) 
    {
        foreach (InventorySlot slot in slots) 
        {
            if (!slot.empty) continue;
            
            slot.PlaceInSlot(item);
            return;
        }
    }
}
