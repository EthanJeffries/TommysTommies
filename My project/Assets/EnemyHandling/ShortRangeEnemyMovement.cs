using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyMovement : EnemyMovement
{
    //Inherits variables and methods from EnemyMovement

    private void FixedUpdate()
    {
        //Vector3 directionOfTarget = (targetTransform.position - transform.position).normalized;
        //enemyDestination = (targetTransform.position - transform.position).normalized;
        //float angleToTarget = Mathf.Atan2(directionOfTarget.y, directionOfTarget.x) * Mathf.Rad2Deg;
        //enemyRB.rotation = angleToTarget;
        //enemyMoveDirection = directionOfTarget;

        if (combatReady)
        {
            enemyAgent.SetDestination(targetTransform.position);

        }
    }
}
