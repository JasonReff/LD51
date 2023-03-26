using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(DestroyWithKey))]
public class DestroyWithKeyEditor : Editor
{
    private DestroyWithKey script;

    private void OnEnable()
    {
        script = (DestroyWithKey)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Connect To Other Locks"))
        {
            script.ConnectToOtherLocks();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}