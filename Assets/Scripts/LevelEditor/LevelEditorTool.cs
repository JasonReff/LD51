using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEditor
{
    public abstract class LevelEditorTool : ScriptableObject
    {
        [SerializeField] private GameObject _highlight;
        [SerializeField] private Sprite _toolSprite;
        public GameObject Highlight { get => _highlight; }
        public Sprite ToolSprite { get => _toolSprite; }
        public bool IsWall;
        public bool IsFloor;
        public bool IsMechanics;
        public virtual void UseTool(Vector3Int tileCoordinates, LevelEditorTilemap tilemap)
        {

        }
    }
}
