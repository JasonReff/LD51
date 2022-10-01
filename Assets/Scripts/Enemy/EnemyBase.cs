using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private bool alwaysVisible = true;

    protected NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    private EnemyState _state;
    [SerializeField] private float _visionRadius;
    public float VisionRadius { get => _visionRadius; }

    protected virtual void Start()
    {
        SetNavMeshAgent();
        ChangeState(new ChaseState(this));
    }

    protected void SetNavMeshAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected virtual void Update()
    {
        _state.UpdateState();
    }

    public void ChangeState(EnemyState newState)
    {
        if (_state != null)
        {
            _state.EndState();
        }
        _state = newState;
        _state.BeginState();
    }
}

public class EnemyState
{
    protected EnemyBase _stateMachine;

    public EnemyState(EnemyBase stateMachine) { _stateMachine = stateMachine; }
    public virtual void BeginState()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void EndState()
    {

    }
}

public class ChaseState : EnemyState
{
    public ChaseState(EnemyBase stateMachine) : base(stateMachine) { }
    public override void UpdateState()
    {
        _stateMachine.Agent.SetDestination(PlayerManager.Instance.transform.position);
    }
}

public class PatrolState : EnemyState
{
    private List<Vector2> _patrolPoints;
    private int _nextPatrolPointIndex;
    private bool _detectsPlayer;
    public PatrolState(EnemyBase stateMachine) : base(stateMachine) { }

    public override void BeginState()
    {
        base.BeginState();
        _patrolPoints = (_stateMachine as PatrolEnemy).PatrolPoints;
        _detectsPlayer = (_stateMachine as PatrolEnemy).DetectsPlayer;
    }

    public override void UpdateState()
    {
        if ((Vector2)_stateMachine.transform.position == _patrolPoints[_nextPatrolPointIndex])
            ChoosePatrolPoint();
        _stateMachine.Agent.SetDestination(_patrolPoints[_nextPatrolPointIndex]);
        AttemptDetectPlayer();
    }

    private void AttemptDetectPlayer()
    {
        if (!_detectsPlayer)
            return;
        if (Vector3.Distance(PlayerManager.Instance.transform.position, _stateMachine.transform.position) <= _stateMachine.VisionRadius)
        {
            _stateMachine.ChangeState(new ChaseState(_stateMachine));
        }
    }

    private void ChoosePatrolPoint()
    {
        _nextPatrolPointIndex++;
        if (_nextPatrolPointIndex >= _patrolPoints.Count)
            _nextPatrolPointIndex = 0;
    }
}