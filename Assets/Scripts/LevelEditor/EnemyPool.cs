using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/EnemyPool")]
public class EnemyPool : ScriptableObject
{
    [SerializeField] private List<EnemyID> _allEnemies;

    public GameObject GetEnemy(int id)
    {
        return _allEnemies.Find(t => t.ID == id).EnemyPrefab;
    }

    [System.Serializable]
    public class EnemyID
    {
        public int ID;
        public GameObject EnemyPrefab;
    }
}