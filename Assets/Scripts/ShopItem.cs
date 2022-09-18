using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Item itemToPurchase;
    [SerializeField] int cost;

    InventoryManager im;
    PlayerInstance pi;

    void Start ()
    {
        im = InventoryManager.instance;
        pi = PlayerInstance.instance;
    }

    void OnMouseOver ()
    {
        if (MyExtensions.WithinDistance(Camera.main.transform.position, transform.position, 5f)) 
        {
            pi.ShowInfoText("'E' to purchase " + itemToPurchase.itemName + " for " + cost + " gold");

            if (Input.GetKeyDown(KeyCode.E) && pi.gold >= cost) 
            {
                im.AddItem(itemToPurchase);
                pi.DeductGold(cost);
                pi.HideInfoText();
                Destroy(gameObject);
            }
        }
    }
    
    void OnMouseExit () 
    {
        pi.HideInfoText();
    }
}
