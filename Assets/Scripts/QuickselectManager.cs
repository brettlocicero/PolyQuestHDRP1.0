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
    [SerializeField] Sprite emptySprite;

    [Header("Animations")]
    [SerializeField] Animation equipAnimController;
    [SerializeField] AnimationClip equipAnim;

    float disableTimer;

    void Start () 
    {
        UpdateSlotSprites();
        UpdateSizes(0);
    }

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
            UpdateSizes(1);
            PlayEquipAnim();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && slot2.item) 
        {
            SwapToWeapon(slot2.item);
            UpdateSlotSprites();

            slot2Visual.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            slot1Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            slot3Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            UpdateSizes(2);
            PlayEquipAnim();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && slot3.item) 
        {
            SwapToWeapon(slot3.item);
            UpdateSlotSprites();

            slot3Visual.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            slot1Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            slot2Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
            UpdateSizes(3);
            PlayEquipAnim();
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
        slot1Visual.transform.GetChild(0).GetComponent<Image>().sprite = (slot1.item) ? slot1.item.sprite : emptySprite;
        slot2Visual.transform.GetChild(0).GetComponent<Image>().sprite = (slot2.item) ? slot2.item.sprite : emptySprite;
        slot3Visual.transform.GetChild(0).GetComponent<Image>().sprite = (slot3.item) ? slot3.item.sprite : emptySprite;
    }

    void UpdateSizes (int slotChosen) 
    {
        Vector2 chosenSize = new Vector2(100f, 100f);
        Vector2 defaultSize = new Vector2(75f, 75f);

        switch (slotChosen) 
        {
            case 0:
                slot1Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot1Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot1Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                slot2Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot2Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot2Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                slot3Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot3Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot3Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                break;
            case 1:
                slot1Visual.GetComponent<Image>().rectTransform.sizeDelta = chosenSize;
                slot1Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = chosenSize;
                slot1Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = chosenSize + new Vector2(5f, 5f);

                slot2Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot2Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot2Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                slot3Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot3Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot3Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                break;
            case 2:
                slot1Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot1Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot1Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                slot2Visual.GetComponent<Image>().rectTransform.sizeDelta = chosenSize;
                slot2Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = chosenSize;
                slot2Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = chosenSize + new Vector2(5f, 5f);

                slot3Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot3Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot3Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                break;
            case 3:
                slot1Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot1Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot1Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                slot2Visual.GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot2Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = defaultSize;
                slot2Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = defaultSize + new Vector2(5f, 5f);

                slot3Visual.GetComponent<Image>().rectTransform.sizeDelta = chosenSize;
                slot3Visual.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = chosenSize;
                slot3Visual.transform.GetChild(1).GetComponent<Image>().rectTransform.sizeDelta = chosenSize + new Vector2(5f, 5f);

                break;
        }
    }

    void PlayEquipAnim () 
    {
        equipAnimController.Rewind(equipAnim.name);
        equipAnimController.Play(equipAnim.name);
    }
        
    public void DraggedSelectedItemOut () 
    {
        UpdateSlotSprites();
        UpdateSizes(0);

        foreach (GameObject obj in weapons) 
            obj.SetActive(false);

        slot1Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
        slot2Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
        slot3Visual.transform.GetChild(1).GetComponent<Image>().color = Color.black;
    }
}
