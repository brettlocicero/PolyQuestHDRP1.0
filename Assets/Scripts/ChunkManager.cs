using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Runtime")]
    [SerializeField] int regionIndex;
    [SerializeField] int roomCount;
    [SerializeField] Transform connectorSpawnPoint;
    [SerializeField] Chunk mostRecentChunk;

    [Header("")]
    [SerializeField] Region[] regions;

    bool lastWasShop;

    void Awake () 
    {
        instance = this;
    }

    public void SpawnChunk ()
    {
        Chunk chunk;
        GameObject chunkObj;

        // spawn boss
        if (roomCount % 5 == 0) 
        {
            chunk = regions[regionIndex].chunks[Random.Range(0, regions[regionIndex].chunks.Length)];
            chunkObj = Instantiate(chunk.gameObject, connectorSpawnPoint.position, Quaternion.identity);
        }

        // spawn shop / healing
        else if ((roomCount + 1) % 5 == 0) 
        {
            // spawn shop
            if (!lastWasShop) 
            {
                chunk = regions[regionIndex].shopChunks[Random.Range(0, regions[regionIndex].shopChunks.Length)];
                chunkObj = Instantiate(chunk.gameObject, connectorSpawnPoint.position, Quaternion.identity);
            }

            // spawn healing room
            else 
            {
                chunk = regions[regionIndex].shopChunks[Random.Range(0, regions[regionIndex].shopChunks.Length)];
                chunkObj = Instantiate(chunk.gameObject, connectorSpawnPoint.position, Quaternion.identity);
            }
        }

        // spawn normal room
        else 
        {
            chunk = regions[regionIndex].chunks[Random.Range(0, regions[regionIndex].chunks.Length)];
            chunkObj = Instantiate(chunk.gameObject, connectorSpawnPoint.position, Quaternion.identity);
        }

        mostRecentChunk = chunkObj.GetComponent<Chunk>();
        SpawnConnector();
        roomCount++;
    }

    void SpawnConnector () 
    {
        Path path = regions[regionIndex].connectors[Random.Range(0, regions[regionIndex].connectors.Length)];
        GameObject pathObj = Instantiate(path.gameObject, mostRecentChunk.connectorSpawnPoint.position, Quaternion.identity);
        connectorSpawnPoint = pathObj.GetComponent<Path>().connectorSpawnPoint;
    }
}

[System.Serializable]
struct Region 
{
    public string regionName;
    public Chunk[] chunks;
    public Chunk[] shopChunks;
    public Path[] connectors;
}