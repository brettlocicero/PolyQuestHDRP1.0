using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RuntimeBakeNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshData navData;
    
    void Awake () 
    {
        //NavMesh.AddNavMeshData(navData);
    }
}
