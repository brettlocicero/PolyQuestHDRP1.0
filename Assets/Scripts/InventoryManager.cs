using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    void Awake () => instance = this;

    [SerializeField] InventorySlot[] slots;
    public Transform itemPanel;
    [SerializeField] CanvasGroup inventoryCanvasGroup;

    bool open;
    float targetAlpha;
    FirstPersonController fpc;

    void Start () 
    {
        fpc = PlayerInstance.instance.GetComponent<FirstPersonController>();
    }

    void Update () 
    {
        inventoryCanvasGroup.alpha = Mathf.Lerp(inventoryCanvasGroup.alpha, targetAlpha, 0.02f);

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            open = !open;
        }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (open) 
        {
            targetAlpha = 1f;
            inventoryCanvasGroup.blocksRaycasts = true;
            CursorManager.UnlockCursor();
            fpc.LockMouselook();
        }

        else 
        {
            targetAlpha = 0f;
            inventoryCanvasGroup.blocksRaycasts = false;
            CursorManager.LockCursor();
            fpc.UnlockMouselook();
        }
    }

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
