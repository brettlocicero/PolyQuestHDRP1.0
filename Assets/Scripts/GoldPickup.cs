using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class GoldPickup : MonoBehaviour
{
    [SerializeField] Vector2 goldRange;

    void OnMouseOver () 
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickupGold();
    }

    void PickupGold () 
    {
        if (!MyExtensions.WithinDistance(PlayerInstance.instance.transform.position, transform.position, 5f)) return;

        int gold = (int)Random.Range(goldRange.x, goldRange.y);
        PlayerInstance.instance.AddGold(gold);
        Destroy(gameObject);
    }
}
