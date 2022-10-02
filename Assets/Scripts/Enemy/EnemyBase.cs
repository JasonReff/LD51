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
    private bool _isPatrolEnemy;
    private Transform _playerTransform;
    public ChaseState(EnemyBase stateMachine) : base(stateMachine) { }

    public override void BeginState()
    {
        base.BeginState();
        if (_stateMachine is PatrolEnemy) _isPatrolEnemy = true;
        _playerTransform = PlayerManager.Instance.transform;
    }
    public override void UpdateState()
    {
        if (_playerTransform == null)
            _playerTransform = PlayerManager.Instance.transform;
        _stateMachine.Agent.SetDestination(_playerTransform.position);
        CheckForPatrolState();
    }

    private void CheckForPatrolState()
    {
        if (_isPatrolEnemy && IsPlayerOutsideVisionRange())
        {
            _stateMachine.ChangeState(new PatrolState(_stateMachine));
        }
    }

    private bool IsPlayerOutsideVisionRange()
    {
        float distance = Vector3.Distance(_playerTransform.position, _stateMachine.transform.position);
        if (distance > _stateMachine.VisionRadius)
            return true;
        return false;
    }
}

public class PatrolState : EnemyState
{
    private List<Vector2> _patrolPoints;
    private int _nextPatrolPointIndex;
    private bool _detectsPlayer;
    private bool _reverseOnFinish;
    private bool _movingInReverse;
    private bool _detectPlayerThroughWall;
    private Transform _playerTransform;
    public PatrolState(EnemyBase stateMachine) : base(stateMachine) { }

    public override void BeginState()
    {
        base.BeginState();
        _patrolPoints = (_stateMachine as PatrolEnemy).PatrolPoints;
        _detectsPlayer = (_stateMachine as PatrolEnemy).DetectsPlayer;
        _reverseOnFinish = (_stateMachine as PatrolEnemy).ReverseOnFinish;
        _detectPlayerThroughWall = (_stateMachine as PatrolEnemy).DetectsPlayerThroughWall;
        _stateMachine.Agent.SetDestination(_patrolPoints[0]);
        _playerTransform = PlayerManager.Instance.transform;
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
        if (_playerTransform == null)
            _playerTransform = PlayerManager.Instance.transform;
        if (Vector3.Distance(_playerTransform.position, _stateMachine.transform.position) <= _stateMachine.VisionRadius)
        {
            if (IsWallObscuring())
                return;
            _stateMachine.ChangeState(new ChaseState(_stateMachine));
        }

        bool IsWallObscuring()
        {
            if (_detectPlayerThroughWall)
                return false;
            List<RaycastHit2D> results = new List<RaycastHit2D>();
            int hits = Physics2D.Linecast(_stateMachine.transform.position, _playerTransform.position, new ContactFilter2D().NoFilter(), results);
            foreach (var hit in results)
            {
                if (hit.transform.gameObject.tag == "Wall")
                {
                    return true;
                }
            }
            return false;
        }
    }

    private void ChoosePatrolPoint()
    {
        if (_movingInReverse)
            _nextPatrolPointIndex--;
        else
            _nextPatrolPointIndex++;
        if (_reverseOnFinish && _nextPatrolPointIndex == 0)
            _movingInReverse = false;
        if (_nextPatrolPointIndex >= _patrolPoints.Count)
        {
            if (!_reverseOnFinish)
                _nextPatrolPointIndex = 0;
            else
            {
                _movingInReverse = true;
                _nextPatrolPointIndex--;
            }
        }
        _stateMachine.Agent.SetDestination(_patrolPoints[_nextPatrolPointIndex]);
    }
}