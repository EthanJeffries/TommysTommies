using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyMovement : MonoBehaviour
{
    [SerializeField] private float movespeed;
    private Rigidbody2D enemyRB;
    private GameObject target;
    private Transform targetTransform;
    private Vector2 enemyMoveDirection;

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetTransform = collision.transform;
        }
    }

    private void Update()
    {
        if (targetTransform && target.GetComponent<PlayerCombat>().weaponReady)
        {
            Vector3 directionOfTarget = (targetTransform.position - transform.position).normalized;
            float angleToTarget = Mathf.Atan2(directionOfTarget.y, directionOfTarget.x) * Mathf.Rad2Deg;
            enemyRB.rotation = angleToTarget;
            enemyMoveDirection = directionOfTarget;
        }
    }

    private void FixedUpdate()
    {
        if (targetTransform)
        {
            enemyRB.velocity = new Vector2(enemyMoveDirection.x, enemyMoveDirection.y) * movespeed;
        }
    }
}
