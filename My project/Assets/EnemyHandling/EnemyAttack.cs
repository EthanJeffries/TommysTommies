using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : EnemyHandling
{
    [SerializeField] protected float enemyAttackRange;
    [SerializeField] protected bool playerInAttackRange;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackCoolDown;
    protected bool alreadyAttacking;

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = CheckWithinRange(enemyAttackRange);

        if (playerInAttackRange) EnemyAttackPlayer();
    }

    protected IEnumerator EnemyAttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        alreadyAttacking = false;
    }

    //This will be overwritten by the children functions
    protected abstract void EnemyAttackPlayer();
}
