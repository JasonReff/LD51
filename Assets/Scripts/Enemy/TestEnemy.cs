using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    protected override void Update()
    {
        base.Update();
        agent.SetDestination(PlayerManager.Instance.transform.position);
    }
}
