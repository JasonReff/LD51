using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyBase
{
    [Header("Pathing")]
    [SerializeField] private List<Transform> _patrolPoints;
    [SerializeField] private bool _reverseOnFinish;
    public List<Vector2> PatrolPoints { get {
            var positions = new List<Vector2>();
            for (int i = 0; i < _patrolPoints.Count; i++)
            {
                positions.Add(_patrolPoints[i].position);
            }
            return positions; } }
    [Space(10)]
    [Header("Player Detection")]
    [SerializeField] private bool _detectsPlayer;
    public bool DetectsPlayer { get => _detectsPlayer; }
    [SerializeField] private float _visionAngle = 90f;
    
    [SerializeField] private bool _detectsPlayerThroughWall;
    public bool OnlyDetectsForward;
    [Space(10)]
    [Header("Player Avoidance")]
    [SerializeField] private bool _runsFromPlayer;
    [SerializeField] private float _wallPivot;
    public float WallPivot { get => _wallPivot; }
    [Space(10)]
    [Header("Debugging")]
    [Tooltip("For debugging, start enemy in avoid state.")]
    [SerializeField] private bool _startInRunState;
    
    [SerializeField] private Color _gizmoColor = new Color(1f, 0f, 0f, 0.25f);
    public float VisionAngle { get => _visionAngle; }
    public bool DetectsPlayerThroughWall { get => _detectsPlayerThroughWall; set => _detectsPlayerThroughWall = value; }
    public bool ReverseOnFinish { get => _reverseOnFinish; }
    public bool RunsFromPlayer { get => _runsFromPlayer; set => _runsFromPlayer = value; }
    
    public event Action OnPatrolPointsChanged;
    public event Action OnWarp;

    protected override void Start()
    {
        SetNavMeshAgent();
        if (_startInRunState)
            ChangeState(new AvoidState(this));
        else
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

    public void SetPatrolPoints(List<Transform> points)
    {
        _patrolPoints = points;
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

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = _gizmoColor;
        foreach (var point in _patrolPoints)
        {
            Gizmos.DrawSphere(point.transform.position, 0.5f);

            var nextPoint = _patrolPoints.GetNext(point);
            if (_reverseOnFinish && _patrolPoints.IndexOf(point) == _patrolPoints.Count - 1)
                continue;
            Gizmos.DrawLine(point.transform.position, nextPoint.transform.position);
        }
    }


}
