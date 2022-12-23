using UnityEngine;

public class EnemyTransformationPoltergeist : EnemyTransformation
{
    [SerializeField] private int _mesh1ID, _mesh2ID;

    protected override void DoTransformation()
    {
        base.DoTransformation();
        if (_navMeshAgent.agentTypeID == _mesh1ID)
            _navMeshAgent.agentTypeID = _mesh2ID;
        else _navMeshAgent.agentTypeID = _mesh1ID;
    }
}