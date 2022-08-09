using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    void Awake () => instance = this;

//////////////////////////////////////////////////////////////////////////////////

    [Header("Runtime")]
    public int roomNum;
    public int regionIndex;
    [SerializeField] GameObject currentRoom;

    [Header("")]
    [SerializeField] Region[] regions;

    public void GenerateRoom ()
    {
        roomNum++;

        Room pickedRoom = regions[regionIndex].rooms[Random.Range(0, regions[regionIndex].rooms.Length)];
        GameObject room = Instantiate(pickedRoom.gameObject, Vector3.zero, Quaternion.identity);
        Destroy(currentRoom);
        currentRoom = room;

        CharacterController player = PlayerInstance.instance.GetComponent<CharacterController>();
        player.enabled = false;
        player.transform.position = room.GetComponent<Room>().playerSpawnPos.position;
        player.transform.rotation = room.GetComponent<Room>().playerSpawnPos.rotation;
        player.enabled = true;
    }
}

[System.Serializable]
struct Region 
{
    public string regionName;
    public Room[] rooms;
}