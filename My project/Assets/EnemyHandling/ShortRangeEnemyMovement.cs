using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyMovement : EnemyMovement
{
    //Inherits variables and methods from EnemyMovement

    [SerializeField] protected float stoppingDistance;


    private void FixedUpdate()
    {
        if (CheckWithinRange(stoppingDistance))
        {
            enemyAgent.SetDestination(transform.position);
        }
    }

    protected override void EnemyAttackMovement()
    {
        enemyAgent.SetDestination(playerTransform.position);
    }
}
