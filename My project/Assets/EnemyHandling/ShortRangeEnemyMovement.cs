using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyMovement : EnemyMovement
{
    //Inherits variables and methods from EnemyMovement

    private void FixedUpdate()
    {
        if (combatReady)
        {
            ChasePlayer();
        }
    }
}
