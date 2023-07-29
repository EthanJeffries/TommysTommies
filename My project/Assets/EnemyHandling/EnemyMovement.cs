using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : MonoBehaviour
{
    //Speed Float Variables
    [SerializeField] protected float regularSpeed, lungeSpeed, lungeCoolDownSpeed;
    
    //Lunge Cooldown Time
    [SerializeField] protected float lungeCoolDown;

    //Combat Checks
    [SerializeField] protected float meleeRange;
    [SerializeField] protected bool combatReady;
    [SerializeField] protected bool playerInMeleeRange;

    //GameObject and Component Variables
    protected Rigidbody2D enemyRB;
    protected GameObject target;
    protected Transform targetTransform;

    //NavMesh Stuff Variables
    [SerializeField] protected LayerMask whatIsGround, whatIsPlayer;
    protected NavMeshAgent enemyAgent;
    protected Vector3 enemyDestination;


    //Enemy runs this upon spawning to intialize variables
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.updateRotation = false;
        enemyAgent.updateUpAxis = false;
        enemyAgent.speed = regularSpeed;
        target = GameObject.FindWithTag("Player");
        targetTransform = target.transform;
    }

    //Different State Methods
    //Enemy Chases Player
    protected void ChasePlayer()
    {
        enemyAgent.SetDestination(targetTransform.position);
    }

    //Lunge mechanics
    private void EnemyLunge()
    {
        enemyAgent.speed = lungeSpeed;
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
        enemyAgent.speed = lungeCoolDownSpeed;
        yield return new WaitForSeconds(lungeCoolDown);
        enemyAgent.speed = regularSpeed;
    }

    private void Update()
    {
        //Combat Range Checks
        playerInMeleeRange = Physics2D.OverlapCircle(transform.position, meleeRange, whatIsPlayer);

        //Check for player weapon ready = combat ready
        if (target.GetComponent<PlayerCombat>().weaponReady)
        {
            combatReady = true;
        }

        if (combatReady)
        {
            Vector3 directionOfTarget = (targetTransform.position - transform.position).normalized;
            float angleToTarget = Mathf.Atan2(directionOfTarget.y, directionOfTarget.x) * Mathf.Rad2Deg;
            enemyRB.rotation = angleToTarget;
        }

        if (playerInMeleeRange)
        {
            EnemyLunge();
        }

    }

}
