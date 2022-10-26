using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEditor
{
    public abstract class LevelEditorTool : ScriptableObject
    {
        [SerializeField] private GameObject _highlight;
        public GameObject Highlight { get => _highlight; }
        public bool IsWall;
        public bool IsFloor;
        public virtual void UseTool(Vector3Int tileCoordinates, Tilemap tilemap)
        {

        }
    }
}
