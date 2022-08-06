using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class RoomDoor : MonoBehaviour
{
    bool opened;

    void OnMouseOver () 
    {
        if (opened) return;

        if (MyExtensions.WithinDistance(transform.position, PlayerInstance.instance.transform.position, 5f)) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                print("door opened");
                RoomManager.instance.GenerateRoom();
                opened = true;
            }
        }
    }
}
