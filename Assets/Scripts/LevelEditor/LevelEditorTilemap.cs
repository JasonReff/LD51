using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEditor
{
    public class LevelEditorTilemap : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private List<GameObject> _gameObjects;
        [SerializeField] private Vector2Int _bottomLeft = new Vector2Int(-9, -27), _topRight = new Vector2Int(47, 4);

        private bool DoesObjectExist(GameObject prefab, Vector2 position)
        {
            foreach (var go in _gameObjects)
            {
                if (go.name.Contains(prefab.transform.name))
                {
                    if ((Vector2)go.transform.position == position)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public GameObject PaintGameObject(GameObject prefab, Vector3Int coordinates)
        {
            var position = _tilemap.GetCellCenterWorld(coordinates);
            if (DoesObjectExist(prefab, position))
                return null;
            var go = Instantiate(prefab, position, prefab.transform.rotation, transform);
            _gameObjects.Add(go);
            return go;
        }

        public void EraseGameObject(Vector3Int coordinates, GameObject prefab = null)
        {
            var position = _tilemap.GetCellCenterLocal(coordinates);
            for (int i = _gameObjects.Count - 1; i >= 0; i--)
            {
                GameObject go = _gameObjects[i];
                if (prefab == null || (prefab != null && go.name.Contains(prefab.transform.name)))
                {
                    if (go.transform.localPosition == position)
                    {
                        Destroy(go);
                        _gameObjects.Remove(go);
                    }
                }
            }
        }

        public void RemoveGameObject(GameObject go)
        {
            _gameObjects.Remove(go);
            Destroy(go);
        }

        public void FillTilemap(GameObject prefab)
        {
            foreach (GameObject go in _gameObjects.ToList())
            {
                RemoveGameObject(go);
            }
            for (int x = _bottomLeft.x; x <= _topRight.x; x++)
            {
                for (int y = _bottomLeft.y; y <= _topRight.y; y++)
                {
                    var position = _tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    var go = Instantiate(prefab, position, prefab.transform.rotation, transform);
                    _gameObjects.Add(go);
                }
            }
        }
    }
}
