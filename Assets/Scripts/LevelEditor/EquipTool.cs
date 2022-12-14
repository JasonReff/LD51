using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class EquipTool : LevelEditorEquippable
    {
        [SerializeField] private LevelEditorTool _tool;
        public LevelEditorTool Tool { get => _tool; }
        [SerializeField] private Image _image;

        public void Initialize(LevelEditorTool tool)
        {
            _tool = tool;
            _image.sprite = tool.ToolSprite;
        }

        public virtual void Equip()
        {
            OnToolEquipped?.Invoke(_tool);
        }
    }
}
