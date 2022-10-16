using System;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] private bool _isActivatable, _isActive;
    [SerializeField] private LightSource _lightSource;
    public static event Action<PlayerCheckpoint> OnCheckpointActivated;

    private void Start()
    {
        _lightSource.enabled = false;
    }

    public void Activate()
    {
        if (!_isActivatable)
            return;
        _isActivatable = false;
        _isActive = true;
        _lightSource.enabled = true;
        OnCheckpointActivated?.Invoke(this);
    }

    public void UseCheckpoint()
    {
        _isActivatable = false;
        _isActive = false;
        _lightSource.enabled = false;
    }

    public void SnuffOut()
    {
        _isActive = false;
        _lightSource.enabled = false;
    }
}
