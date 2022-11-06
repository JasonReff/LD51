using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "LevelEditor/EditorMenu")]
    public class LevelEditorMenu : LevelEditorTool
    {
        [SerializeField] private List<LevelEditorTool> _tools;
        [SerializeField] private LevelEditorTool _selectedTool;

        public List<LevelEditorTool> Tools { get => _tools; }
    }
}
