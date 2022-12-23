using UnityEngine;

public class EnemyTransformationPoltergeist : EnemyTransformation
{
    private int _mesh1ID, _mesh2ID;
    [SerializeField] private string _mesh1Name, _mesh2Name;
    [SerializeField] private PatrolEnemy _patrolEnemy;

    private void Start()
    {
        _mesh1ID = AgentTypeID.GetAgentTypeIDByName(_mesh1Name);
        _mesh2ID = AgentTypeID.GetAgentTypeIDByName(_mesh2Name);
    }
    protected override void DoTransformation()
    {
        base.DoTransformation();
        if (_navMeshAgent.agentTypeID == _mesh1ID)
            _navMeshAgent.agentTypeID = _mesh2ID;
        else _navMeshAgent.agentTypeID = _mesh1ID;
        _patrolEnemy.DetectsPlayerThroughWall = _isBat;
    }
}
