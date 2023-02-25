using StormlightManor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelEditorManager : MonoBehaviour
    {
        [SerializeField] private LevelEditorTool _equippedTool;
        public LevelEditorTool EquippedTool { get => _equippedTool; }
        [SerializeField] private Tilemap _highlightTilemap;
        [SerializeField] private LevelEditorTilemap _wallTilemap, _floorTilemap, _mechanicsTilemap;
        [SerializeField] private LightningStrikeManager _lightningManager;
        [SerializeField] private Canvas _editorCanvas;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private CompositeShadowToggle _shadowToggle;
        [SerializeField] private NavMeshManager _nav;
        private Camera _main;
        private GameObject _toolHighlight;


        private void OnEnable()
        {
            _main = Camera.main;
            LevelEditorEquippable.OnToolEquipped += SetTool;
            SingleUseTool.OnToolUsed += OnFloorFillTool;
            TestButton.BakeTilemap += OnTestStart;
            TestButton.OnTestEnd += OnTestEnd;
            EditorEnemy.OnEnemyClicked += OnEnemyClicked;
        }

        private void OnDisable()
        {
            LevelEditorEquippable.OnToolEquipped -= SetTool;
            SingleUseTool.OnToolUsed -= OnFloorFillTool;
            TestButton.BakeTilemap -= OnTestStart;
            TestButton.OnTestEnd -= OnTestEnd;
            EditorEnemy.OnEnemyClicked -= OnEnemyClicked;
        }

        private void Start()
        {
            _audioManager.enabled = false;
        }

        private void SetTool(LevelEditorTool tool)
        {
            _equippedTool = tool;
            if (_toolHighlight != null)
                Destroy(_toolHighlight);
            if (_equippedTool != null && _equippedTool.Highlight != null)
                _toolHighlight = Instantiate(_equippedTool.Highlight);
        }

        private void Unequip()
        {
            SetTool(null);
        }

        private void OnEnemyClicked(EditorEnemy enemy)
        {
            Unequip();
        }

        private void OnTestStart()
        {
            Unequip();
            _audioManager.enabled = true;
            _lightningManager.enabled = true;
            _editorCanvas.enabled = false;
            _shadowToggle.enabled = true;
            _nav.RebakePlaymodeNavMeshes();
        }

        private void OnTestEnd()
        {
            _audioManager.enabled = false;
            _lightningManager.enabled = false;
            _editorCanvas.enabled = true;
            _shadowToggle.enabled = false;
        }

        private void Update()
        {
            var mousePosition = _main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tile = _highlightTilemap.WorldToCell(mousePosition);
            if (_toolHighlight != null)
            {
                if (_toolHighlight)
                    _toolHighlight.transform.position = _highlightTilemap.GetCellCenterWorld(tile);
            }
            if (_equippedTool != null && Input.GetMouseButton(0))
            {
                if (IsMouseOverUI())
                    return;
                if (_equippedTool.IsFloor)
                    _equippedTool.UseTool(tile, _floorTilemap);
                if (_equippedTool.IsWall)
                    _equippedTool.UseTool(tile, _wallTilemap);
                if (_equippedTool.IsMechanics)
                    _equippedTool.UseTool(tile, _mechanicsTilemap);
            }
        }

        private void OnFloorFillTool(LevelEditorTool tool)
        {
            Unequip();
            tool.UseTool(Vector3Int.zero, _floorTilemap);
        }

        private bool IsMouseOverUI()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }

    public class LevelEditorSaveLoad : SingletonMonobehaviour<LevelEditorSaveLoad>
    {
        private SavedLevel _loadedLevel;


    }

    public class LevelReader
    {
        public List<SavedLevel> AllLevels;
    }

    [System.Serializable]
    public class SavedLevel
    {
        public int FloorID;
        public List<TileCoordinates> Wall;
        public List<EnemyCoordinates> Enemies;

        public class TileCoordinates
        {
            public int TileID;
            public Vector2 TilePosition;
        }
        public class EnemyCoordinates
        {
            public int EnemyID;
            public List<Vector2> Positions;
        }
    }

    [System.Serializable]
    public class TilePool : ScriptableObject
    {
        [SerializeField] private List<TileID> _allTiles;
        [System.Serializable]
        public class TileID
        {
            public int ID;
            public GameObject TilePrefab;
        }

        public GameObject GetTile(int id)
        {
            return _allTiles.Find(t => t.ID == id).TilePrefab;
        }
    }
}
