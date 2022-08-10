using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public int damage;
    public float swingSpeed;
    [TextArea] public string itemDesc;
    public Sprite sprite;

    [Header("Stats")]
    public float dmg;
    public float attackSpeed;
    public float range;
    public int rarity;
}
