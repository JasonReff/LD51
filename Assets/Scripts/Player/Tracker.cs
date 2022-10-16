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

    public void StopTrackingPoint(TrackableGameObject trackable)
    {
        _patrolEnemy.RemovePatrolPoint(trackable.transform);
    }

    
}