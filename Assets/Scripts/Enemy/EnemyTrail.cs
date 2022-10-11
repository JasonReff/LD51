using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrail : MonoBehaviour
{
    [SerializeField] private GameObject _trailPrefab;
    [SerializeField] private float _spawnRate;
    private Coroutine _trailCoroutine;
    [SerializeField] private bool _trailPriority;
    [SerializeField] private EnemyBase _enemy;

    private void Start()
    {
        _trailCoroutine = StartCoroutine(TrailCoroutine());
    }

    private IEnumerator TrailCoroutine()
    {
        var trail = Instantiate(_trailPrefab, transform.position, Quaternion.identity);
        if (_trailPriority && trail.TryGetComponent(out EnemyPriority priority))
            priority.Enemy = _enemy;
        yield return new WaitForSeconds(_spawnRate);
        _trailCoroutine = StartCoroutine(TrailCoroutine());
    }
}