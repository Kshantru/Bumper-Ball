using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class EnemyController : MonoBehaviour
{

    //public float lookRadius = 10f;  // Detection range for player
    public LayerMask whatIsGround, whatIsPlayer, whatIsBall;

    Transform target;   // Reference to the player
    Transform targetBall; // Reference to ball
    NavMeshAgent agent; // Reference to the NavMeshAgent
                        //CharacterCombat combat;

    //Patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange, ballRange;
    public bool playerInSightRange, playerInAttackRange, ballInSightRange;

    // Use this for initialization
    void Start()
    {
        //target = PlayerManager.instance.player.transform;
        target = GameObject.Find("Player").transform;
        targetBall = GameObject.Find("Football").transform;
        agent = GetComponent<NavMeshAgent>();
        //combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);
        float distanceToBall = Vector3.Distance(targetBall.position, transform.position);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        ballInSightRange = Physics.CheckSphere(transform.position, ballRange, whatIsBall);


        if (!playerInSightRange && !playerInAttackRange && !ballInSightRange) Patroling();
        if (playerInSightRange && !playerInAttackRange && ballInSightRange)
        {
            if (distance <= distanceToBall)
            {
                ChasePlayer();
            }
            else
            {
                ChaseBall();
            }
        }
        if (playerInSightRange && !playerInAttackRange && !ballInSightRange) ChasePlayer();
        if (!playerInSightRange && !playerInAttackRange && ballInSightRange) ChaseBall();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        //if (playerInSightRange && !playerInAttackRange &&ballInSightRange) ChaseBall();

        // If inside the lookRadius
        //if (distance <= lookRadius)
        //{
        //	// Move towards the target
        //	agent.SetDestination(target.position);

        //	// If within attacking distance
        //	if (distance <= agent.stoppingDistance)
        //	{
        //		//CharacterStats targetStats = target.GetComponent<CharacterStats>();
        //		//if (targetStats != null)
        //		//{
        //			//combat.Attack(targetStats);
        //		//}

        //		FaceTarget();   // Make sure to face towards the target
        //	}
        //      }
        //      else
        //      {
        //	Patroling();
        //}
    }

    // Rotate to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ballRange);
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //walkPoint could go out of map, so..
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }

    private void ChaseBall()
    {
        agent.SetDestination(targetBall.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            //Attack Code here
            //gameObject.GetComponent<Animator>().SetTrigger("TriggerName");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    //For friendlies you need to make 2 new LayerMasks (EnemyRed and EnemyBlue), 
    //then instead of whatIsplayer you just check for EnemyRed or EnemyBlue :D
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}