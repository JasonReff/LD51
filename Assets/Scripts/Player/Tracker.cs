using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private PatrolEnemy _patrolEnemy;
    private void OnEnable()
    {
        TrackableGameObject.OnObjectSpawned += StartTrackingPoint;
    }

    private void OnDisable()
    {
        TrackableGameObject.OnObjectSpawned -= StartTrackingPoint;
    }

    private void StartTrackingPoint(TrackableGameObject trackable)
    {
        _patrolEnemy.AddPatrolPoint(trackable.transform);
    }

    private void StopTrackingPoint(TrackableGameObject trackable)
    {
        _patrolEnemy.RemovePatrolPoint(trackable.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out TrackableGameObject trackable))
        {
            StopTrackingPoint(trackable);
            Destroy(trackable.gameObject);
        }
    }
}