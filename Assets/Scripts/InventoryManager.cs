using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    void Awake () => instance = this;

    [Header("Inventory References")]
    [SerializeField] InventoryItem iItem;
    [SerializeField] InventorySlot[] slots;
    public Transform itemPanel;
    [SerializeField] CanvasGroup inventoryCanvasGroup;
    [SerializeField] CanvasGroup mainHUDCanvasGroup;
    [SerializeField] Volume postProcessing;
    public CanvasGroup infoPanelCanvasGroup;

    [Header("Item Info Panel")]
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescText;

    [Header("Inventory Player Model")]
    [SerializeField] GameObject inventoryCam;
    [SerializeField] GameObject[] playerModelWeapons;

    [Header("Rarities")]
    public Color[] rarityColors;

    [Header("Pickup Notifications")]
    [SerializeField] Transform layoutGroup;
    [SerializeField] PickupNotifObj baseNotifItem;

    [HideInInspector] public bool open;
    float targetAlphaInven;
    float targetAlphaMainHUD;
    FirstPersonController fpc;
    DepthOfField dof;

    void Start () 
    {
        fpc = PlayerInstance.instance.GetComponent<FirstPersonController>();
        postProcessing.profile.TryGet(out dof);
    }

    void Update () 
    {
        inventoryCanvasGroup.alpha = Mathf.Lerp(inventoryCanvasGroup.alpha, targetAlphaInven, 10f * Time.deltaTime);
        mainHUDCanvasGroup.alpha = Mathf.Lerp(mainHUDCanvasGroup.alpha, targetAlphaMainHUD, 10f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            open = !open;
            inventoryCam.SetActive(open);
            dof.active = open;
        }

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (open) 
        {
            targetAlphaInven = 1f;
            targetAlphaMainHUD = 0f;
            inventoryCanvasGroup.blocksRaycasts = true;
            mainHUDCanvasGroup.blocksRaycasts = false;
            CursorManager.UnlockCursor();
            fpc.LockMouselook();
        }

        else 
        {
            targetAlphaInven = 0f;
            targetAlphaMainHUD = 1f;
            inventoryCanvasGroup.blocksRaycasts = false;
            mainHUDCanvasGroup.blocksRaycasts = true;
            CursorManager.LockCursor();
            fpc.UnlockMouselook();
        }
    }

    public void AddItem (Item item) 
    {
        TriggerPickupNotif(item.sprite, item.name);

        foreach (InventorySlot slot in slots) 
        {
            if (!slot.empty) continue;

            GameObject itemObj = Instantiate(iItem.gameObject, Vector3.zero, Quaternion.identity);
            itemObj.GetComponent<InventoryItem>().UpdateItemDisplay(item);
            itemObj.transform.SetParent(slot.transform);
            itemObj.transform.localPosition = Vector3.zero;
            itemObj.transform.localScale = Vector3.one;
            slot.item = item;
            
            slot.empty = false;
            QuickselectManager.instance.UpdateSlotSprites();
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

    public void TriggerPickupNotif (Sprite pic, string notifName = "Undefined Item") 
    {
        var obj = Instantiate(baseNotifItem.gameObject, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(layoutGroup);
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<PickupNotifObj>().UpdateItemNotifObj(pic, notifName);

        Destroy(obj, 4.017f);
    }
}
