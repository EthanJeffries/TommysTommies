using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    //Basic variables
    [SerializeField] protected float regularSpeed;
    [SerializeField] protected float lungeSpeed;
    [SerializeField] protected float lungeCoolDownSpeed;
    [SerializeField] protected float lungeCoolDown;
    protected bool combatReady;
    protected float moveSpeed;
    protected Rigidbody2D enemyRB;
    protected GameObject target;
    protected Transform targetTransform;
    protected Vector2 enemyMoveDirection;

    //Enemy runs this upon spawning
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetTransform = target.transform;
        moveSpeed = regularSpeed;
    }

    //Lunge mechanics
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
        moveSpeed = lungeCoolDownSpeed;
        yield return new WaitForSeconds(lungeCoolDown);
        moveSpeed = lungeSpeed;
    }

    private void Update()
    {
        //Will find player even if offscreen and has weapons out
        if (target.GetComponent<PlayerCombat>().weaponReady)
        {
            combatReady = true;
        }

    }

}
