using System;
using UnityEngine;

public class EnemyPriority : MonoBehaviour
{
    [SerializeField] private bool _triggerOnPlayerCollision;
    [SerializeField] private EnemyBase _enemy;
    public EnemyBase Enemy { get => _enemy; set => _enemy = value; }
    public static event Action<EnemyPriority> OnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_triggerOnPlayerCollision)
            if (collision.TryGetComponent(out PlayerManager player))
                OnTrigger?.Invoke(this);
    }
}