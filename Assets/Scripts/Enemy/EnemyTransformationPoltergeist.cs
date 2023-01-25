using UnityEngine;

public class EnemyTransformationPoltergeist : EnemyTransformation
{
    private int _mesh1ID, _mesh2ID;
    [SerializeField] private string _mesh1Name, _mesh2Name;
    [SerializeField] private Material _material1, _material2;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private PatrolEnemy _patrolEnemy;

    private void Start()
    {
        _mesh1ID = AgentTypeID.GetAgentTypeIDByName(_mesh1Name);
        _mesh2ID = AgentTypeID.GetAgentTypeIDByName(_mesh2Name);
    }
    protected override void DoTransformation()
    {
        base.DoTransformation();
        if (_isBat)
        {
            _navMeshAgent.agentTypeID = _mesh2ID;
            _sr.material = _material2;
        }
        else 
        { 
            _navMeshAgent.agentTypeID = _mesh1ID;
            _sr.material = _material1;
        }
        _patrolEnemy.DetectsPlayerThroughWall = _isBat;
    }
}
