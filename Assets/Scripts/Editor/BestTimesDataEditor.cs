using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BestTimesData))]

public class BestTimesDataEditor : Editor
{
    private BestTimesData script;

    private void OnEnable()
    {
        script = (BestTimesData)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset Times"))
        {
            script.ResetTimes();
        }
        if (GUILayout.Button("Add Missing Levels"))
        {
            script.AddMissingStages();
        }
    }
}