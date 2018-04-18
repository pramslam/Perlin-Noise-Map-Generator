using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor {

    // Creates a generate map button within the editor
    public override void OnInspectorGUI() {
        MapGenerator mapGen = (MapGenerator)target;

        // Auto updates if any value is changed
        if (DrawDefaultInspector()) {
            if (mapGen.autoUpdate) {
                mapGen.GenerateMap();
            }
        }

        // Add editor button
        if (GUILayout.Button ("Generate")) {
            mapGen.GenerateMap();
        }
    }
}
