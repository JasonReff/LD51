using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyBase
{
    [SerializeField] private List<Vector2> _patrolPoints;
    public List<Vector2> PatrolPoints { get => _patrolPoints; }
    [SerializeField] private bool _detectsPlayer;
    public bool DetectsPlayer { get => _detectsPlayer; }

    protected override void Start()
    {
        SetNavMeshAgent();
        ChangeState(new PatrolState(this));
    }
}
