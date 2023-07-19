using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeEnemyMovement : MonoBehaviour
{
    [SerializeField] private float regularSpeed;
    [SerializeField] private float lungeSpeed;
    [SerializeField] private float coolDownspeed;
    [SerializeField] private float lungeCoolDown;
    private bool combatReady;
    private float moveSpeed;
    private Rigidbody2D enemyRB;
    private GameObject target;
    private Transform targetTransform;
    private Vector2 enemyMoveDirection;

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetTransform = target.transform;
        moveSpeed = regularSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = lungeSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = regularSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(LungeCoolDown());
        }
    }

    private IEnumerator LungeCoolDown()
    {
        moveSpeed = coolDownspeed;
        yield return new WaitForSeconds(lungeCoolDown);
        moveSpeed = lungeSpeed;
    }

    private void Update()
    {
        if (targetTransform && target.GetComponent<PlayerCombat>().weaponReady)
        {
            combatReady = true;
        }
        
        if (combatReady)
        {
            Vector3 directionOfTarget = (targetTransform.position - transform.position).normalized;
            float angleToTarget = Mathf.Atan2(directionOfTarget.y, directionOfTarget.x) * Mathf.Rad2Deg;
            enemyRB.rotation = angleToTarget;
            enemyMoveDirection = directionOfTarget;
        }
    }

    private void FixedUpdate()
    {
        if (combatReady)
        {
            enemyRB.velocity = new Vector2(enemyMoveDirection.x, enemyMoveDirection.y) * moveSpeed;
        }
    }
}
