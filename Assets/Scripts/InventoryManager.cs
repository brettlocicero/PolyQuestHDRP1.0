using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    void Awake () => instance = this;

    [SerializeField] InventorySlot[] slots;
    public Transform itemPanel;

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
