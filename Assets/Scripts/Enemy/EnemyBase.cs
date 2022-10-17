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
    protected EnemyState _state;
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
    protected Transform _playerTransform;
    protected bool _runsFromPlayer;
    protected bool _detectPlayerThroughWall;
    protected bool _detectsPlayer;
    protected bool _onlyDetectsForward;
    protected float _visionAngleThreshold = 90f;

    public EnemyState(EnemyBase stateMachine) { _stateMachine = stateMachine; }
    public virtual void BeginState()
    {
        _playerTransform = PlayerManager.Instance.transform;
    }

    public virtual void UpdateState()
    {

    }

    public virtual void EndState()
    {

    }

    protected void AttemptDetectPlayer()
    {
        if (!_detectsPlayer | !PlayerManager.Instance.IsVisibleToEnemies)
            return;
        if (_playerTransform == null)
            _playerTransform = PlayerManager.Instance.transform;
        if (Vector3.Distance(_playerTransform.position, _stateMachine.transform.position) <= _stateMachine.VisionRadius)
        {
            if (IsWallObscuring())
                return;
            if (!IsPlayerInFront())
                return;
            if (_runsFromPlayer)
                _stateMachine.ChangeState(new AvoidState(_stateMachine));
            else
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

        bool IsPlayerInFront()
        {
            if (!_onlyDetectsForward)
                return true;
            Vector2 velocity = Vector3.Normalize(_stateMachine.Agent.velocity);
            var visionDirection = velocity - (Vector2)_stateMachine.transform.position;
            var playerDirection = _playerTransform.position - _stateMachine.transform.position;
            var playerAngle = Vector2.Angle(visionDirection, playerDirection);
            if (playerAngle < _visionAngleThreshold)
            {
                return true;
            }
            return false;
        }
    }
}

public class ChaseState : EnemyState
{
    private bool _isPatrolEnemy;
    public ChaseState(EnemyBase stateMachine) : base(stateMachine) { }

    public override void BeginState()
    {
        base.BeginState();
        if (_stateMachine is PatrolEnemy) _isPatrolEnemy = true;
    }
    public override void UpdateState()
    {
        if (_playerTransform == null)
            _playerTransform = PlayerManager.Instance.transform;
        _stateMachine.Agent.SetDestination(_playerTransform.position);
        CheckForPatrolState();
        CheckForAvoidState();
    }

    private void CheckForPatrolState()
    {
        if (_isPatrolEnemy && IsPlayerOutsideVisionRange())
        {
            _stateMachine.ChangeState(new PatrolState(_stateMachine));
        }
    }

    private void CheckForAvoidState()
    {
        if (_isPatrolEnemy && (_stateMachine as PatrolEnemy).RunsFromPlayer)
            _stateMachine.ChangeState(new AvoidState(_stateMachine));
    }

    private bool IsPlayerOutsideVisionRange()
    {
        if (!PlayerManager.Instance.IsVisibleToEnemies)
            return true;
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
    public int NextPoint { get => _nextPatrolPointIndex; }
    private bool _reverseOnFinish;
    private bool _movingInReverse;
    private PatrolEnemy _patrolEnemy;
    public PatrolState(EnemyBase stateMachine, int nextPoint = 0) : base(stateMachine) { _nextPatrolPointIndex = nextPoint; }

    public override void BeginState()
    {
        base.BeginState();
        _patrolEnemy = (_stateMachine as PatrolEnemy);
        _patrolEnemy.OnPatrolPointsChanged += ResetPatrolPoints;
        _patrolEnemy.OnWarp += ChoosePatrolPoint;
        _patrolPoints = _patrolEnemy.PatrolPoints;
        _detectsPlayer = _patrolEnemy.DetectsPlayer;
        _reverseOnFinish = _patrolEnemy.ReverseOnFinish;
        _detectPlayerThroughWall = _patrolEnemy.DetectsPlayerThroughWall;
        _runsFromPlayer = _patrolEnemy.RunsFromPlayer;
        _visionAngleThreshold = _patrolEnemy.VisionAngle;
        _onlyDetectsForward = _patrolEnemy.OnlyDetectsForward;
        _stateMachine.Agent.SetDestination(_patrolPoints[0]);
        
    }

    public override void UpdateState()
    {
        if (_patrolPoints.Count == 0 | _nextPatrolPointIndex >= _patrolPoints.Count) return;
        if ((Vector2)_stateMachine.transform.position == _patrolPoints[_nextPatrolPointIndex])
            ChoosePatrolPoint();
        _stateMachine.Agent.SetDestination(_patrolPoints[_nextPatrolPointIndex]);
        AttemptDetectPlayer();
    }

    public override void EndState()
    {
        base.EndState();
        _patrolEnemy.OnPatrolPointsChanged -= ResetPatrolPoints;
        _patrolEnemy.OnWarp -= ResetPatrolPoints;
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

    private void ResetPatrolPoints()
    {
        _patrolPoints = _patrolEnemy.PatrolPoints;
    }
}

public class AvoidState : EnemyState
{
    private float _optimalDistance = 2f;
    public AvoidState(EnemyBase stateMachine) : base(stateMachine)
    {
        
    }

    public override void UpdateState()
    {
        if (_playerTransform == null)
            _playerTransform = PlayerManager.Instance.transform;
        RunFromPlayer();
        CheckForPatrolState();
        CheckForChaseState();
    }

    private void RunFromPlayer() 
    {
        var vectorFromPlayer = Vector3.Normalize(_stateMachine.transform.position - _playerTransform.position);
        var newPosition = _stateMachine.transform.position + vectorFromPlayer * _optimalDistance;
        _stateMachine.Agent.SetDestination(newPosition);
    }

    private void CheckForPatrolState()
    {
        if (IsPlayerOutsideVisionRange())
        {
            _stateMachine.ChangeState(new PatrolState(_stateMachine));
        }
    }
    
    private void CheckForChaseState()
    {
        if (_stateMachine is PatrolEnemy && !(_stateMachine as PatrolEnemy).RunsFromPlayer)
            _stateMachine.ChangeState(new ChaseState(_stateMachine));
    }

    private bool IsPlayerOutsideVisionRange()
    {
        if (!PlayerManager.Instance.IsVisibleToEnemies)
            return true;
        float distance = Vector3.Distance(_playerTransform.position, _stateMachine.transform.position);
        if (distance > _stateMachine.VisionRadius)
            return true;
        return false;
    }
}

public class DestinationState : EnemyState
{
    private Vector3 _destination;
    private int _nextPatrolPoint;
    public int NextPoint { get => _nextPatrolPoint; }
    public DestinationState(EnemyBase stateMachine, Transform destination, int nextPoint = 0) : base(stateMachine)
    {
        _destination = destination.position;
        _nextPatrolPoint = nextPoint;
    }

    public override void BeginState()
    {
        base.BeginState();
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_destination == null | _destination == _stateMachine.transform.position)
        {
            _stateMachine.ChangeState(new PatrolState(_stateMachine, _nextPatrolPoint));
            return;
        }
        AttemptDetectPlayer();
        _stateMachine.Agent.SetDestination(_destination);
    }

}
