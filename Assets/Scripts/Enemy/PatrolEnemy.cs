using System;
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
    [SerializeField] private bool _detectsPlayerThroughWall;
    [SerializeField] private bool _runsFromPlayer;
    public bool DetectsPlayerThroughWall { get => _detectsPlayerThroughWall; }
    public bool ReverseOnFinish { get => _reverseOnFinish; }
    public bool RunsFromPlayer { get => _runsFromPlayer; }
    public event Action OnPatrolPointsChanged;
    public event Action OnWarp;

    protected override void Start()
    {
        SetNavMeshAgent();
        ChangeState(new PatrolState(this));
    }

    private void OnEnable()
    {
        EnemyPriority.OnTrigger += OnPriorityTrigger;
    }

    private void OnDisable()
    {
        EnemyPriority.OnTrigger -= OnPriorityTrigger;
    }

    public void AddPatrolPoint(Transform patrolPoint)
    {
        _patrolPoints.Add(patrolPoint);
        OnPatrolPointsChanged?.Invoke();
    }

    private void OnPriorityTrigger(EnemyPriority priority)
    {
        if (priority.Enemy == this)
            InterruptPatrol(priority.transform);
    }

    public void InterruptPatrol(Transform destination)
    {
        int patrolPoint = 0;
        if (_state is PatrolState)
            patrolPoint = (_state as PatrolState).NextPoint;
        else if (_state is DestinationState)
            patrolPoint = (_state as DestinationState).NextPoint;
        ChangeState(new DestinationState(this, destination, patrolPoint));
    }

    public void RemovePatrolPoint(Transform patrolPoint)
    {
        _patrolPoints.Remove(patrolPoint);
        OnPatrolPointsChanged?.Invoke();
    }

    public void WarpPatrolEnemy()
    {
        OnWarp?.Invoke();
    }
}
