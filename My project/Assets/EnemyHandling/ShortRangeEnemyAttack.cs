using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyAttack : EnemyAttack
{

    protected override void EnemyAttackPlayer()
    {
        if (!alreadyAttacking)
        {
            alreadyAttacking = true;
            Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, enemyAttackRange, whatIsPlayer);
            player[0].gameObject.GetComponent<HealthSystem>().DealDamage(attackDamage);
            StartCoroutine(EnemyAttackCoolDown());
        }
    }
    
}
