using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChunkTrigger : MonoBehaviour
{
    bool spawned;

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !spawned) 
        {
            ChunkManager.instance.SpawnChunk();
            spawned = true;
        }
    }
}
