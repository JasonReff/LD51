using System;

namespace LevelEditor
{
    public class SingleUseTool : EquipTool
    {
        public static event Action<LevelEditorTool> OnToolUsed;
        public override void Equip()
        {
            OnToolUsed?.Invoke(Tool);
        }
    }
}
