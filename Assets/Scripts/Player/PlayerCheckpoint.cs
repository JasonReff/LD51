using System;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] private bool _isActivatable, _isActive;
    [SerializeField] private LightSource _lightSource;
    [SerializeField] private Animator _animator;
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
        SetAnimation();
        _lightSource.enabled = true;
        OnCheckpointActivated?.Invoke(this);
    }

    public void UseCheckpoint()
    {
        _isActivatable = false;
        _isActive = false;
        SetAnimation();
        _lightSource.enabled = false;
    }

    public void SnuffOut()
    {
        if (_isActive || _isActivatable)
            _isActivatable = true;
        _isActive = false;
        SetAnimation();
        _lightSource.enabled = false;
    }

    private void SetAnimation()
    {
        _animator.SetBool("Broken", !_isActivatable);
    }
}
