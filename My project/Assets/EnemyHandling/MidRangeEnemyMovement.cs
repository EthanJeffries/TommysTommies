using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidRangeEnemyMovement : MonoBehaviour
{
    //Inherits variables and methods from EnemyMovement

    //roam around/patrol

    //strafe and shoot

    //lunge capability

    public UnityEngine.AI.NavMeshAgent agent;

    public Transform player;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        //player = findgameobjectwithtag(player).transform
        //agent = getcomponent(navmeshagent)
    }

    private void Update()
    {
        //check for player in sight/attack range
        //playerInSightRange = Physics.checksphere(transform.position, sightRange, whatIsPlayer); check interact function for 2D capabilities
        //playerInAttackRange = Physics.checksphere(transform.position, attackRange, whatIsPlayer); check interact function for 2D capabilities

        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);

        //walkPoint = new Vector3(transform.position.x + randomX, transform.y + randomY, transform.z);

        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        //{
        //    walkPointSet = true;
        //}
    }


    private void AttackPlayer()
    {
        //Enemy doesn't move
        agent.SetDestination(transform.position);

        //transform.LookAt(Player);

        if(!alreadyAttacked)
        {
            //Attack code goes here
            //video using example of shooting
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
}
