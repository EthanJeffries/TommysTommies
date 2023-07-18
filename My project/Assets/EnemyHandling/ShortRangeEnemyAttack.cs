using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthSystem>().DealDamage(attackDamage);
        }
    }
}
