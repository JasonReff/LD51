using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "LevelEditor/PlaceTile")]
    public class PlaceTileTool : LevelEditorTool
    {
        [SerializeField] private GameObject _tilePrefab;

        public override void UseTool(Vector3Int tileCoordinates, Tilemap tilemap)
        {
            base.UseTool(tileCoordinates, tilemap);
            var tile = Instantiate(_tilePrefab, tilemap.GetCellCenterWorld(tileCoordinates), _tilePrefab.transform.rotation, tilemap.transform);
        }

    }
}
