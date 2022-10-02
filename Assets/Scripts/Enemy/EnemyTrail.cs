using System.Collections;
using UnityEngine;

public class EnemyTrail : MonoBehaviour
{
    [SerializeField] private GameObject _trailPrefab;
    [SerializeField] private float _spawnRate;
    private Coroutine _trailCoroutine;

    private void Start()
    {
        _trailCoroutine = StartCoroutine(TrailCoroutine());
    }

    private IEnumerator TrailCoroutine()
    {
        Instantiate(_trailPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_spawnRate);
        _trailCoroutine = StartCoroutine(TrailCoroutine());
    }
}