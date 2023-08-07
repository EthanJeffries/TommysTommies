using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidRangeEnemyAttack : EnemyAttack
{
    //Attacking
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject enemyGun;
    [SerializeField] private Transform enemyBulletFirePoint;

    [SerializeField] private float bulletForce;

    protected override void EnemyAttackPlayer()
    {
        if (!alreadyAttacking)
        {
            alreadyAttacking = true;
            FireBullet();
            StartCoroutine(EnemyAttackCoolDown());
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyBulletFirePoint.position, enemyBulletFirePoint.rotation);
        bullet.GetComponent<BulletBehavior>().setBulletDamage(attackDamage);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        bulletRB.AddForce(enemyBulletFirePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (playerInAttackRange)
        {
            enemyGun.SetActive(true);
        }
        else
        {
            enemyGun.SetActive(false);
        }
    }
}
