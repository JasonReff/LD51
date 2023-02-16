using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour, IMovementController
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _speedModifier, _defaultAcceleration, _slippingAcceleration, _defaultTurnSpeed, _slippingTurnSpeed;
    private bool _isSlipping;
    public bool IsSlipping { get => _isSlipping;}
    public void SetSpeed(float speed, bool slippery)
    {
        _navMeshAgent.speed = speed * _speedModifier;
        _isSlipping = slippery;
        if (_isSlipping)
        {
            _navMeshAgent.acceleration = _slippingAcceleration;
            _navMeshAgent.angularSpeed = _slippingTurnSpeed;
        }
        else
        {
            _navMeshAgent.acceleration = _defaultAcceleration;
            _navMeshAgent.angularSpeed = _defaultTurnSpeed;
        }
    }
}
