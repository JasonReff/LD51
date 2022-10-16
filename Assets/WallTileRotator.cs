using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTileRotator : MonoBehaviour
{
    [SerializeField] private Tilemap _wallTilemap;
    private List<Transform> _walls = new List<Transform>();
    private int _wallsRotatedCount;

    public void RotateTiles()
    {
        _walls.Clear();
        _wallsRotatedCount = 0;
        foreach (Transform wallTransform in _wallTilemap.transform)
        {
            _walls.Add(wallTransform);
        }
        foreach (var wall in _walls)
            RotateBasedOnNeighboringTiles(wall);
        Debug.Log(_wallsRotatedCount + " Walls Rotated");
    }

    private void RotateBasedOnNeighboringTiles(Transform wall)
    {
        var wallPosition = wall.localPosition;
        bool isWallUp = DoesWallExist(wallPosition, new Vector2(0, 1));
        bool isWallDown = DoesWallExist(wallPosition, new Vector2(0, -1));
        bool isWallLeft = DoesWallExist(wallPosition, new Vector2(-1, 0));
        bool isWallRight = DoesWallExist(wallPosition, new Vector2(1, 0));
        if (isWallLeft | isWallRight)
        {
            wall.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (isWallUp | isWallDown)
        {
            wall.eulerAngles = new Vector3(0, 0, 90f);
            _wallsRotatedCount++;
        }
    }

    private bool DoesWallExist(Vector2 wallPosition, Vector2 relativePosition)
    {
        if (_walls.Any(t => t.localPosition == new Vector3(wallPosition.x + relativePosition.x, wallPosition.y + relativePosition.y, 0)))
            return true;
        return false;
    }
}
