using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelEditor
{
    public class LevelEditorTilemap : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private List<GameObject> _gameObjects;

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

        public void PaintGameObject(GameObject prefab, Vector3Int coordinates)
        {
            var position = _tilemap.GetCellCenterWorld(coordinates);
            if (DoesObjectExist(prefab, position))
                return;
            var go = Instantiate(prefab, position, prefab.transform.rotation, transform);
            _gameObjects.Add(go);
        }

        public void EraseGameObject(Vector3Int coordinates, GameObject prefab = null)
        {
            var position = _tilemap.GetCellCenterLocal(coordinates);
            foreach (var go in _gameObjects)
            {
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
    }
}
