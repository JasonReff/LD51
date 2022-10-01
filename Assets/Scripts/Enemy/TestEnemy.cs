using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    void Update()
    {
        agent.SetDestination(PlayerManager.Instance.transform.position);
    }
}
