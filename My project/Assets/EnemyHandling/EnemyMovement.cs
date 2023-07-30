using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : EnemyHandling
{
    //Range values for distance to player checks
    [SerializeField] protected float enemySightRange;

    //Boolean values for distance checks
    protected bool playerInSightRange;
    
    //GameObject and Component Variables
    protected Rigidbody2D enemyRB;
    protected Transform playerTransform;
    protected GameObject player;


    //NavMesh Stuff Variables
    [SerializeField] protected LayerMask whatIsGround;
    protected NavMeshAgent enemyAgent;

    //Might not need? Could be used for roaming function
    protected Vector3 enemyDestination;

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
    }

    private void LookAtPlayer()
    {
        Vector3 directionOfTarget = (playerTransform.position - transform.position).normalized;
        float angleToTarget = Mathf.Atan2(directionOfTarget.y, directionOfTarget.x) * Mathf.Rad2Deg;
        enemyRB.rotation = angleToTarget;
    }

    //This will be overwritten by the children functions
    protected abstract void EnemyAttackMovement();

}
