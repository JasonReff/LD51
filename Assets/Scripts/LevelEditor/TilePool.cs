using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(menuName = "GameData/TilePool")]
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
