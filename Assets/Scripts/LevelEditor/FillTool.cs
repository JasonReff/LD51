using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "LevelEditor/FillTool")]
    public class FillTool : LevelEditorTool
    {
        [SerializeField] private GameObject _floor;
        public override void UseTool(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {
            tilemap.FillTilemap(_floor);
        }
    }
}
