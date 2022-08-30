using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [SerializeField] Chunk[] chunks;
    [SerializeField] Path[] connectors;
    [SerializeField] Transform connectorSpawnPoint;
    [SerializeField] Chunk mostRecentChunk;

    void Awake () 
    {
        instance = this;
    }

    public void SpawnChunk ()
    {
        GameObject chunkObj = Instantiate(chunks[0].gameObject, connectorSpawnPoint.position, Quaternion.identity);
        mostRecentChunk = chunkObj.GetComponent<Chunk>();
        SpawnConnector();
    }

    void SpawnConnector () 
    {
        GameObject path = Instantiate(connectors[0].gameObject, mostRecentChunk.connectorSpawnPoint.position, Quaternion.identity);
        connectorSpawnPoint = path.GetComponent<Path>().connectorSpawnPoint;
    }
}
