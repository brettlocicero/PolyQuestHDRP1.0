using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    void Awake () => instance = this;

    [Header("Inventory References")]
    [SerializeField] InventoryItem iItem;
    [SerializeField] InventorySlot[] slots;
    public Transform itemPanel;
    [SerializeField] CanvasGroup inventoryCanvasGroup;

    [Header("Item Info Panel")]
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescText;

    [Header("Inventory Player Model")]
    [SerializeField] GameObject inventoryCam;
    [SerializeField] GameObject[] playerModelWeapons;

    [HideInInspector] public bool open;
    float targetAlpha;
    FirstPersonController fpc;

    void Start () 
    {
        fpc = PlayerInstance.instance.GetComponent<FirstPersonController>();
    }

    void Update () 
    {
        inventoryCanvasGroup.alpha = Mathf.Lerp(inventoryCanvasGroup.alpha, targetAlpha, 10f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            open = !open;
            inventoryCam.SetActive(open);
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

            GameObject itemObj = Instantiate(iItem.gameObject, Vector3.zero, Quaternion.identity);
            itemObj.GetComponent<InventoryItem>().UpdateItemDisplay(item);
            itemObj.transform.SetParent(slot.transform);
            itemObj.transform.localPosition = Vector3.zero;
            itemObj.transform.localScale = Vector3.one;
            
            slot.empty = false;
            return;
        }
    }

    public void UpdateItemInfoPanel (Item item) 
    {
        itemNameText.text = item.itemName;
        itemDescText.text = item.itemDesc;

        foreach (GameObject obj in playerModelWeapons) 
        {
            if (obj.name == item.itemName)
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }
    }
}
