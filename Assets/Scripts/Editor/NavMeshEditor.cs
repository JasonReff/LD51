using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(NavMeshManager))]
public class NavMeshEditor : Editor
{
    private NavMeshManager _script;

    private void OnEnable()
    {
        _script = (NavMeshManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Rebake Meshes"))
        {
            _script.RebakeAllMeshes();
            Debug.Log("Meshes rebaked");
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}