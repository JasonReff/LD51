using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "LevelEditor/PlayerStart")]
    public class PlayerStartTool : LevelEditorTool
    {
        [SerializeField] private GameObject _playerStartPrefab;
        private GameObject _playerStart;
        [SerializeField] private bool _singleton;

        public override void UseTool(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {
            base.UseTool(tileCoordinates, tilemap);
            if (_playerStart != null)
            {
                if (_singleton)
                    tilemap.RemoveGameObject(_playerStart);
            }
            var newStart = tilemap.PaintGameObject(_playerStartPrefab, tileCoordinates);
            _playerStart = newStart;
        }

    }
}
