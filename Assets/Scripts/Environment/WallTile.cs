using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WallTile : MonoBehaviour
{
    [SerializeField] private List<Sprite> _wallSprites;
    [SerializeField] private string folderPath;

#if UNITY_EDITOR
    public void CreateWallPrefabs()
    {
        var assetPath = AssetDatabase.GetAssetPath(this);
        var source = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
        for (int i = 0; i < _wallSprites.Count; i++)
        {
            Sprite sprite = _wallSprites[i];
            var prefabPath = folderPath + i.ToString() + ".prefab";
            var objSource = (GameObject)PrefabUtility.InstantiatePrefab(source);
            objSource.GetComponent<SpriteRenderer>().sprite = sprite;
            PrefabUtility.SaveAsPrefabAsset(objSource, prefabPath);
        }
    }
#endif
}
