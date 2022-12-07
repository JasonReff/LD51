using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "LevelEditor/PlayerStart")]
    public class PlayerStartTool : LevelEditorTool
    {
        [SerializeField] private GameObject _playerStartPrefab;
        private GameObject _playerStart;

        public override void UseTool(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {
            base.UseTool(tileCoordinates, tilemap);
            if (_playerStart != null)
            {
                tilemap.RemoveGameObject(_playerStart);
            }
            var newStart = tilemap.PaintGameObject(_playerStartPrefab, tileCoordinates);
            _playerStart = newStart;
        }

    }
}
