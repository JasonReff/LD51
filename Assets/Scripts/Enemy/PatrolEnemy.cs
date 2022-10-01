using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyBase
{
    [SerializeField] private List<Transform> _patrolPoints;
    public List<Vector2> PatrolPoints { get {
            var positions = new List<Vector2>();
            for (int i = 0; i < _patrolPoints.Count; i++)
            {
                positions.Add(_patrolPoints[i].position);
            }
            return positions; } }
    [SerializeField] private bool _detectsPlayer;
    public bool DetectsPlayer { get => _detectsPlayer; }
    [SerializeField] private bool _reverseOnFinish;
    public bool ReverseOnFinish { get => _reverseOnFinish; }

    protected override void Start()
    {
        SetNavMeshAgent();
        ChangeState(new PatrolState(this));
    }
}
