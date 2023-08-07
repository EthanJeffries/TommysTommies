using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidRangeEnemyMovement : EnemyMovement
{
    //Inherits variables and methods from EnemyMovement

    //using enemySightRange for max and stoppingDistance for min
    [SerializeField] protected float enemyStrafeRange;


    protected override void EnemyAttackMovement()
    {
        Strafe();
    }

    protected void Strafe()
    {
        if (EnemyDestinationCheck())
        {
            Walk();
        }
        else
        {
            FindEnemyDestination(enemyStrafeRange);
        }
    }

    private bool EnemyDestinationCheck()
    {
        Vector3 distanceFromDestinationToEnemy = playerTransform.position - enemyDestination;
        bool withinStrafeRange = (stoppingDistance < distanceFromDestinationToEnemy.magnitude) && (distanceFromDestinationToEnemy.magnitude < enemySightRange);
        return (enemyDestinationSet && withinStrafeRange);
    }
}
