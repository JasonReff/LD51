using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWalkTrigger : MonoBehaviour
{
    [SerializeField] private bool _isActivatedOnEnter;
    [SerializeField] private bool _isActivatedOnExit;
    [SerializeField] private bool _doesPlayerActivate;
    [SerializeField] private bool _doesEnemyActivate;
    public UnityEvent<GameObject> OnTileActivated;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_isActivatedOnEnter)
            return;
        if (collider.TryGetComponent(out PlayerManager player))
        {
            if (_doesPlayerActivate)
                ActivateTrigger(player.gameObject);
        }
        else if (collider.TryGetComponent(out EnemyBase enemy))
        {
            if (_doesEnemyActivate)
                ActivateTrigger(enemy.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!_isActivatedOnExit)
            return;
        if (collider.TryGetComponent(out PlayerManager player))
        {
            if (_doesPlayerActivate)
                ActivateTrigger(player.gameObject);
        }
        else if (collider.TryGetComponent(out EnemyBase enemy))
        {
            if (_doesEnemyActivate)
                ActivateTrigger(enemy.gameObject);
        }
    }

    public virtual void ActivateTrigger(GameObject gameObject)
    {
        OnTileActivated?.Invoke(gameObject);
    }
}
