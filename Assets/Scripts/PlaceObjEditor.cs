using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectPlacer))]
public class PlaceObjEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectPlacer myScript = (ObjectPlacer)target;
        if (GUILayout.Button("Generate"))
        {
            myScript.Generate();
        }
    }
}
