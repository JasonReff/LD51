using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Vector2> _targetPositions = new List<Vector2>();
    public List<Vector2> TargetPositions => _targetPositions;

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

    protected virtual void OnDrawGizmos()
    {
        
        foreach (var position in _targetPositions)
        {
            if (_targetPositions.IndexOf(position) == 0)
                Gizmos.color = new Color(0, 1, 0, 0.25f);
            else
                Gizmos.color = new Color(0, 0, 1, 0.25f);
            Gizmos.DrawSphere(position, 0.25f);
            Gizmos.DrawLine(PlayerManager.Instance.transform.position, position);
        }
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

    protected void SetDestination(Vector2 destination)
    {
        _stateMachine.Agent.SetDestination(destination);
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
    public PatrolState(EnemyBase stateMachine, int nextPoint = 0, bool movingInReverse = false) : base(stateMachine) 
    { 
        _nextPatrolPointIndex = nextPoint; 
        _movingInReverse = movingInReverse; 
    }

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
        SetDestination(_patrolPoints[0]);
        
    }

    public override void UpdateState()
    {
        if (_patrolPoints.Count == 0 | _nextPatrolPointIndex >= _patrolPoints.Count) return;
        if ((Vector2)_stateMachine.transform.position == _patrolPoints[_nextPatrolPointIndex])
            ChoosePatrolPoint();
        SetDestination(_patrolPoints[_nextPatrolPointIndex]);
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
            if (_nextPatrolPointIndex > 0)
                _nextPatrolPointIndex--;
            else
                _nextPatrolPointIndex = _patrolPoints.Count - 1;
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
        SetDestination(_patrolPoints[_nextPatrolPointIndex]);
    }

    private void ResetPatrolPoints()
    {
        _patrolPoints = _patrolEnemy.PatrolPoints;
    }
}

public class AvoidState : EnemyState
{
    private PatrolEnemy _patrolEnemy;
    private List<Vector2> _patrolPoints;
    private float _optimalDistance = 1.5f, _runCheckRate = 0.01f;
    private bool _isRunning;
    private List<Vector2> _verticalDirections = new List<Vector2>() { Vector2.up + new Vector2(0.0001f, 0f), Vector2.down + new Vector2(0.0001f, 0f) };
    private List<Vector2> _horizontalDirections = new List<Vector2>() { Vector2.left, Vector2.right };
    public AvoidState(EnemyBase stateMachine) : base(stateMachine)
    {
        _patrolEnemy = _stateMachine as PatrolEnemy;
        _patrolPoints = _patrolEnemy.PatrolPoints;
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
        if (_isRunning == false)
        {
            _stateMachine.StartCoroutine(RunCoroutine());
        }
    }

    private IEnumerator RunCoroutine()
    {
        _isRunning = true;
        var transform = _stateMachine.transform;
        var vectorFromPlayer = Vector3.Normalize(transform.position - _playerTransform.position);
        var newPosition = transform.position + vectorFromPlayer * _optimalDistance;
        List<Vector2> pivotedDirections = new List<Vector2>();
        var movementPositions = new List<Vector2>();
        foreach (var direction in _verticalDirections)
        {
            var adjustedDirection = direction * _optimalDistance + (Vector2)transform.position;
            movementPositions.Add(PivotOffWall(adjustedDirection, Vector2.right));
        }
        foreach (var direction in _horizontalDirections)
        {
            var adjustedDirection = direction * _optimalDistance + (Vector2)transform.position;
            movementPositions.Add(PivotOffWall(adjustedDirection, Vector2.up));
        }
        movementPositions.RemoveAll(t => IsHittingWall(t));
        movementPositions = movementPositions.OrderByDescending(t => Vector2.SqrMagnitude((Vector2)_playerTransform.position - t)).ToList();
        if (movementPositions.Count > 0) newPosition = movementPositions[0];
        _stateMachine.TargetPositions.Clear();
        _stateMachine.TargetPositions.AddRange(movementPositions);
        SetDestination(newPosition);
        yield return new WaitForSeconds(_runCheckRate);
        _isRunning = false;
    }

    private Vector2 PivotOffWall(Vector2 inputVector, Vector2 perpendicularAxis)
    {
        if (IsHittingWall(inputVector + perpendicularAxis))
        {
            return (inputVector - perpendicularAxis);
        }
        else if (IsHittingWall(inputVector - perpendicularAxis))
            return inputVector + perpendicularAxis;
        else return inputVector;
    }

    private bool IsHittingWall(Vector2 newPosition)
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        int hits = Physics2D.Linecast(_stateMachine.transform.position, newPosition, new ContactFilter2D().NoFilter(), results);
        foreach (var hit in results)
        {
            if (hit.transform.gameObject.tag == "Wall")
            {
                return true;
            }
        }
        return false;
    }

    private void CheckForPatrolState()
    {
        if (IsPlayerOutsideVisionRange())
        {
            var closestPoint = GetClosestPatrolPointIndex();
            _stateMachine.ChangeState(new PatrolState(_stateMachine, closestPoint, PatrolInReverse(_patrolPoints[closestPoint])));
        }
    }

    private int GetClosestPatrolPointIndex()
    {
        if (_patrolPoints.Count < 2)
            return 0;
        var twoClosestPoints = _patrolPoints.OrderBy(t => Vector2.SqrMagnitude((Vector2)_stateMachine.transform.position - t)).Take(2).ToList();
        var furthestFromPlayer = twoClosestPoints.OrderByDescending(t => Vector2.SqrMagnitude(t - (Vector2)PlayerManager.Instance.transform.position)).First();
        var pointIndex = _patrolPoints.IndexOf(furthestFromPlayer);
        return pointIndex;
    }

    private bool PatrolInReverse(Vector2 closestPoint)
    {
        if (_patrolPoints.Count < 3)
            return false;
        var nextPoint = _patrolPoints.GetNext(closestPoint);
        var previousPoint = _patrolPoints.GetPrev(closestPoint);
        if (Vector2.SqrMagnitude((Vector2)PlayerManager.Instance.transform.position - previousPoint) > Vector2.SqrMagnitude((Vector2)PlayerManager.Instance.transform.position - nextPoint))
        {
            return true;
        }
        return false;
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
        SetDestination(_destination);
    }

}
