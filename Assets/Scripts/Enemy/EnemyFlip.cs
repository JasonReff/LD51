using UnityEngine.AI;

public class EnemyFlip : CharacterFlip
{
    private NavMeshAgent _navMeshAgent;

    protected override void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void SetDirection()
    {
        float horizontalVelocity = _navMeshAgent.desiredVelocity.x;
        if (horizontalVelocity < 0)
            transform.localScale = _flippedScale;
        else if (horizontalVelocity > 0)
            transform.localScale = _scale;
    }
}