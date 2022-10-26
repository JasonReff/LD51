using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCompletionData))]
public class LevelCompletionEditor : Editor
{
    LevelCompletionData script;

    private void OnEnable()
    {
        script = (LevelCompletionData)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset Completion Data"))
        {
            script.ResetAllCompletionData();
        }
        if (GUILayout.Button("Reset Characters"))
        {
            script.ResetAllCharacterData();
        }
        if (GUILayout.Button("Add Missing Stages"))
        {
            script.AddMissingStages();
        }
    }
}