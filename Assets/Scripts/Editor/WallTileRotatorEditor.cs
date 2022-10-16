using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallTileRotator))]
public class WallTileRotatorEditor : Editor
{
    private WallTileRotator script;

    private void OnEnable()
    {
        script = (WallTileRotator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Rotate Walls"))
            script.RotateTiles();
    }
}