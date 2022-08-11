using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickselectManager : MonoBehaviour
{
    public static QuickselectManager instance;
    void Awake () => instance = this;

    [SerializeField] InventorySlot slot1;
    [SerializeField] InventorySlot slot2;
    [SerializeField] InventorySlot slot3;
    [SerializeField] GameObject[] weapons;

    [SerializeField] Transform slot1Visual;
    [SerializeField] Transform slot2Visual;
    [SerializeField] Transform slot3Visual;


    float disableTimer;

    void Update () 
    {
        disableTimer -= Time.deltaTime;
        if (disableTimer >= 0f) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && slot1.item) 
        {
            SwapToWeapon(slot1.item);
            UpdateSlotSprites();

            slot1Visual.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            slot2Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            slot3Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && slot2.item) 
        {
            SwapToWeapon(slot2.item);
            UpdateSlotSprites();

            slot2Visual.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            slot1Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            slot3Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && slot3.item) 
        {
            SwapToWeapon(slot3.item);
            UpdateSlotSprites();

            slot3Visual.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            slot1Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            slot2Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
        }
    }

    void SwapToWeapon (Item item) 
    {
        foreach (GameObject obj in weapons) 
        {
            if (obj.name == item.itemName)
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }    
    }

    public void DisableQuickselect (float t) 
    {
        disableTimer = t;
    }

    public void UpdateSlotSprites () 
    {
        slot1Visual.transform.GetChild(0).GetComponent<Image>().sprite = (slot1.item) ? slot1.item.sprite : null;
        slot2Visual.transform.GetChild(0).GetComponent<Image>().sprite = (slot2.item) ? slot2.item.sprite : null;
        slot3Visual.transform.GetChild(0).GetComponent<Image>().sprite = (slot3.item) ? slot3.item.sprite : null;
    }
}
