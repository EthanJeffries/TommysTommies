using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : EnemyHandling
{
    //Range values for distance to player checks
    [SerializeField] protected float enemySightRange;
    [SerializeField] protected float enemyPatrolRange;
    [SerializeField] protected float stoppingDistance;

    //Boolean values for distance checks
    protected bool playerInSightRange;
    
    //GameObject and Component Variables
    protected Rigidbody2D enemyRB;
    protected Transform playerTransform;
    protected GameObject player;


    //NavMesh Stuff Variables
    [SerializeField] protected LayerMask whatIsGround, whatIsWall;
    protected NavMeshAgent enemyAgent;

    //Might not need? Could be used for roaming function
    protected Vector3 enemyDestination;
    protected bool enemyDestinationSet;

    //Enemy runs this upon spawning to intialize variables
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.updateRotation = false;
        enemyAgent.updateUpAxis = false;
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
    }

    private void Update()
    {
        playerInSightRange = CheckWithinRange(enemySightRange);

        if (playerInSightRange)
        {
            LookAtPlayer();
            EnemyAttackMovement();
        }
        else
        {
            Patrol();
        }
    }

    private void LookAtPlayer()
    {
        Vector3 directionOfTarget = (playerTransform.position - transform.position).normalized;
        float angleToTarget = Mathf.Atan2(directionOfTarget.y, directionOfTarget.x) * Mathf.Rad2Deg;
        enemyRB.rotation = angleToTarget;
    }

    protected void Patrol()
    {
        if (!enemyDestinationSet)
        {
            FindEnemyDestination(enemyPatrolRange);
        }
        else
        {
            Walk();
        }
    }

    protected void Walk()
    {
        enemyAgent.SetDestination(enemyDestination);

        Vector3 distanceToWalkPoint = transform.position - enemyDestination;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            enemyDestinationSet = false;
        }
    }

    protected void FindEnemyDestination(float moveRange)
    {
        //Calculate random point in range
        float randomX = Random.Range(-moveRange, moveRange);
        float randomY = Random.Range(-moveRange, moveRange);

        enemyDestination = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
        if (!Physics.Raycast(transform.position, enemyDestination, enemyPatrolRange, whatIsGround) && Physics2D.OverlapCircle(enemyDestination, 0.5f, whatIsWall) == null)
        {
            enemyDestinationSet = true;
        }
    }

    //This will be overwritten by the children functions
    protected abstract void EnemyAttackMovement();

}
