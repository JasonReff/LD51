using System;
using UnityEngine;

namespace LevelEditor
{
    public class EquipTool : MonoBehaviour
    {
        [SerializeField] private LevelEditorTool _tool;
        public static event Action<LevelEditorTool> OnToolEquipped;

        public void Equip()
        {
            OnToolEquipped?.Invoke(_tool);
        }
    }
}
