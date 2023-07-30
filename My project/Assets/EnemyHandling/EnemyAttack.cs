using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : EnemyHandling
{
    [SerializeField] protected float enemyAttackRange;
    [SerializeField] protected bool playerInAttackRange;

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = CheckWithinRange(enemyAttackRange);

        if (playerInAttackRange) EnemyAttackPlayer();
    }

    //This will be overwritten by the children functions
    protected abstract void EnemyAttackPlayer();
}
