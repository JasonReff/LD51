using UnityEngine;
using System;

public class TrackableGameObject : MonoBehaviour
{
    public static Action<TrackableGameObject> OnObjectSpawned;

    private void OnEnable()
    {
        OnObjectSpawned?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Tracker tracker))
        {
            tracker.StopTrackingPoint(this);
            Destroy(gameObject);
        }
    }
}
