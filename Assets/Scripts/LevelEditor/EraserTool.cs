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
}
