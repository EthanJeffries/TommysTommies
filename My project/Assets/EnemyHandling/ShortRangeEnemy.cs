using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemy : MonoBehaviour
{

    [SerializeField] private float maxHealth = 5f;

    [SerializeField] private float movespeed = 3f;
    private Rigidbody2D enemyRB;
    private GameObject target;
    private Transform targetTransform;
    private Vector2 enemyMoveDirection;

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        targetTransform = target.transform;
    }

    private void Update()
    {
        if(targetTransform && target.GetComponent<PlayerCombat>().weaponReady)
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
