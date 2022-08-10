using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    [TextArea] public string itemDesc;
    public Sprite sprite;

    [Header("Stats")]
    public float damage;
    public float attackSpeed;
    public float range;
    public int rarity;
}
