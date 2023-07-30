using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyAttack : EnemyAttack
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCoolDown;
    private bool alreadyAttacking;

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

    private IEnumerator EnemyAttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        alreadyAttacking=false;
    }
    
}
