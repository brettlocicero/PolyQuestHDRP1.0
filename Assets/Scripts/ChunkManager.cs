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
        Chunk chunk = chunks[Random.Range(0, chunks.Length)];
        GameObject chunkObj = Instantiate(chunk.gameObject, connectorSpawnPoint.position, Quaternion.identity);
        mostRecentChunk = chunkObj.GetComponent<Chunk>();
        SpawnConnector();
    }

    void SpawnConnector () 
    {
        Path path = connectors[Random.Range(0, connectors.Length)];
        GameObject pathObj = Instantiate(path.gameObject, mostRecentChunk.connectorSpawnPoint.position, Quaternion.identity);
        connectorSpawnPoint = pathObj.GetComponent<Path>().connectorSpawnPoint;
    }
}
