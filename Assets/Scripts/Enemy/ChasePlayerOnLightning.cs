using UnityEngine;

public class ChasePlayerOnLightning : MonoBehaviour
{
    [SerializeField] private PatrolEnemy _patrolEnemy;

    private void OnEnable()
    {
        LightningStrikeManager.OnLightningStrikeStart += OnLightning;
    }

    private void OnDisable()
    {
        LightningStrikeManager.OnLightningStrikeStart -= OnLightning;
    }

    private void OnLightning()
    {
        _patrolEnemy.InterruptPatrol(PlayerManager.Instance.transform);
    }
}