using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "LevelEditor/Eraser")]
    public class EraserTool : LevelEditorTool
    {
        [SerializeField] private bool _eraseAllObjects;
        private GameObject _selectedPrefab;
        
        public override void UseTool(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {
            base.UseTool(tileCoordinates, tilemap);
            EraseObject(tileCoordinates, tilemap);
        }

        private void EraseObject(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {

            if (_eraseAllObjects)
            {
                tilemap.EraseGameObject(tileCoordinates);
            }
            else
            {
                tilemap.EraseGameObject(tileCoordinates, _selectedPrefab);
            }
        }
    }

    public class PlayerStartTool : LevelEditorTool
    {
        [SerializeField] private PlayerStart _playerStartPrefab;
        private PlayerStart _playerStart;

        public override void UseTool(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {
            base.UseTool(tileCoordinates, tilemap);
            if (_playerStart != null)
            {
                Destroy(_playerStart.gameObject);
            }
            var newStart = Instantiate(_playerStartPrefab);
            _playerStart = newStart;
        }

    }
}
