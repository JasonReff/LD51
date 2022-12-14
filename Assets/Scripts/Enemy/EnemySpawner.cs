using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnLocation;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _spawnDelay;

    private IEnumerator Start()
    {
        _spawnLocation = PlayerManager.Instance.transform.position;
        yield return new WaitForSeconds(_spawnDelay);
        Instantiate(_enemy, _spawnLocation, Quaternion.identity);
        AudioManager.SwapMusic();
    }
}
