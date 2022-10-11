using UnityEngine;
using UnityEngine.AI;

public class ScreenLoop : MonoBehaviour
{
    [SerializeField] private float _minX, _maxX, _minY, _maxY, _buffer;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private PatrolEnemy _patrolEnemy;

    private void Update()
    {
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        if (currentX <= _minX)
        {
            _navMeshAgent.Warp(new Vector2(_maxX - _buffer, currentY));
            _patrolEnemy?.WarpPatrolEnemy();
        }
        else if (currentX >= _maxX)
        {
            _navMeshAgent.Warp(new Vector2(_minX + _buffer, currentY));
            _patrolEnemy?.WarpPatrolEnemy();
        }
        else if (currentY <= _minY)
        {
            _navMeshAgent.Warp(new Vector2(currentX, _maxY - _buffer));
            _patrolEnemy?.WarpPatrolEnemy();
        }
        else if (currentY >= _maxY)
        {
            _navMeshAgent.Warp(new Vector2(currentX, _minY + _buffer));
            _patrolEnemy?.WarpPatrolEnemy();
        }
    }
}