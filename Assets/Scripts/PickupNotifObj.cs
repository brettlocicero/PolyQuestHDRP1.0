using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PickupNotifObj : MonoBehaviour
{
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI itemNameText;

    public void UpdateItemNotifObj (Sprite pic, string t)
    {
        itemSprite.sprite = pic;
        itemNameText.text = t;
    }
}
