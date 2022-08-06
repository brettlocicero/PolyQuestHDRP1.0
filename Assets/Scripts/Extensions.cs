using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions 
{
    public static class MyExtensions
    {
        public static bool WithinDistance (Vector3 a, Vector3 b, float range) 
        {
            return Vector3.Distance(a, b) <= range;
        } 
    }
}