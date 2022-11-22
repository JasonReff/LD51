using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelSelectData")]
public class LevelSelectData : ScriptableObject
{
    [SerializeField] private List<Sprite> _floorSprites, _enemySprites;
    public List<Sprite> FloorSprites { get => _floorSprites; }
    public List<Sprite> EnemySprites { get => _enemySprites; }
}