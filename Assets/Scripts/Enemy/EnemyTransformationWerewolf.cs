using UnityEngine;

public class EnemyTransformationWerewolf : EnemyTransformation
{
    [SerializeField] private PatrolEnemy _patrolEnemy;
    [SerializeField] private DamageCharacter _damageCharacter;
    [SerializeField] private bool _isCoward;
    protected override void DoTransformation()
    {
        base.DoTransformation();
        _isCoward = _isBat;
        _patrolEnemy.RunsFromPlayer = _isCoward;
        _damageCharacter.enabled = !_isCoward;
    }
}