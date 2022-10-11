using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WallTile))]
public class WallTileEditor : Editor
{
    private WallTile script;

    private void OnEnable()
    {
        script = (WallTile)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Wall Prefabs"))
        {
            script.CreateWallPrefabs();
        }
    }
}