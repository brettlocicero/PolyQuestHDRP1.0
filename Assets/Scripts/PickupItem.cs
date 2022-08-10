using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class PickupItem : MonoBehaviour
{
    [SerializeField] Item item;

    void OnMouseOver () 
    {
        if (Input.GetKeyDown(KeyCode.E) && MyExtensions.WithinDistance(transform.position, PlayerInstance.instance.transform.position, 3f)) 
        {
            PickUp();
            Destroy(gameObject);
        }
    }

    void PickUp () 
    {
        InventoryManager.instance.AddItem(item);
    }
}
