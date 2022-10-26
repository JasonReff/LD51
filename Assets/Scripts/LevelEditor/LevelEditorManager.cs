using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEditor
{
    public class LevelEditorManager : MonoBehaviour
    {
        [SerializeField] private LevelEditorTool _equippedTool;
        [SerializeField] private Tilemap _wallTilemap, _floorTilemap, _highlightTilemap;
        private Camera _main;
        private GameObject _toolHighlight;

        private void OnEnable()
        {
            _main = Camera.main;
            EquipTool.OnToolEquipped += SetTool;
        }

        private void OnDisable()
        {
            EquipTool.OnToolEquipped -= SetTool;
        }

        private void SetTool(LevelEditorTool tool)
        {
            _equippedTool = tool;
            if (_toolHighlight != null)
                Destroy(_toolHighlight);
            if (_equippedTool.Highlight != null)
                _toolHighlight = Instantiate(_equippedTool.Highlight);
        }

        private void Update()
        {
            var mousePosition = _main.ScreenToWorldPoint(Input.mousePosition);
            var tile = _highlightTilemap.WorldToCell(mousePosition);
            if (_toolHighlight != null)
            {
                if (_toolHighlight)
                    _toolHighlight.transform.position = _highlightTilemap.GetCellCenterWorld(tile);
            }
            if (_equippedTool != null && Input.GetMouseButtonDown(0))
            {
                if (_equippedTool.IsFloor)
                    _equippedTool.UseTool(tile, _floorTilemap);
                if (_equippedTool.IsWall)
                    _equippedTool.UseTool(tile, _wallTilemap);
            }
        }
    }
}
