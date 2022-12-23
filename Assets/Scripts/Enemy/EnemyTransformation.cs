using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTransformation : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] private float _vampireSpeed, _batSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] protected bool _isBat;

    private void OnEnable()
    {
        LightningStrikeManager.OnLightningStrikeStart += DoTransformation;
    }

    private void OnDisable()
    {
        LightningStrikeManager.OnLightningStrikeStart -= DoTransformation;
    }

    protected virtual void DoTransformation()
    {
        _isBat = !_isBat;
        _animator.SetBool("BatForm", _isBat);
        if (_isBat)
        {
            _navMeshAgent.speed = _batSpeed;
        }
        else
        {
            _navMeshAgent.speed = _vampireSpeed;
        }
    }
}
