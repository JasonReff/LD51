using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DestroyedOnTouch : MonoBehaviour
{
    [SerializeField] private GameObject _objectSpawnedOnDeath;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _deathDuration;
    [SerializeField] private EnemyBase _enemyBase;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private bool _dying;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            if (!_dying)
                StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        _dying = true;
        if (_enemyBase)
            _enemyBase.enabled = false;
        if (_navMeshAgent)
            _navMeshAgent.enabled = false;
        _animator.SetBool("Dying", true);
        yield return new WaitForSeconds(_deathDuration);
        Instantiate(_objectSpawnedOnDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}