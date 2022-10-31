using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "LevelEditor/PlaceTile")]
    public class PlaceTileTool : LevelEditorTool
    {
        [SerializeField] private GameObject _tilePrefab;

        public override void UseTool(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {
            base.UseTool(tileCoordinates, tilemap);
            tilemap.PaintGameObject(_tilePrefab, tileCoordinates);
        }

    }
}
