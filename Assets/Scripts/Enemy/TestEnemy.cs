using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerManager.Instance.transform.position, 0.04f);
    }
}
