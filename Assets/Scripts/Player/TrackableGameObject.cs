using UnityEngine;
using System;

public class TrackableGameObject : MonoBehaviour
{
    public static Action<TrackableGameObject> OnObjectSpawned;

    private void OnEnable()
    {
        OnObjectSpawned?.Invoke(this);
    }
}
