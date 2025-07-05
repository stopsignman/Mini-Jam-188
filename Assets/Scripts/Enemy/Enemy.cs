using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked = false;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private EnemyAnimator anim;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            ChasePlayer();
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        anim.OnWander();
        agent.speed = 11;
        if (!walkPointSet)
        {
            SetWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
        }
    }

    private void SetWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        anim.StopWander();
        agent.SetDestination(transform.position);
        anim.OnAlert();
        StartCoroutine(WaitForAlertAnim());
        agent.SetDestination(player.position);
        agent.speed = 12;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        Vector3 lookDirection = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);

        if (!alreadyAttacked)
        {
            anim.OnAttack();
            StartCoroutine(WaitForAnimation());


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        anim.StopAttack();
        alreadyAttacked = false;
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(.5f);
        if (playerInSightRange && playerInAttackRange)
        {
            Damager damager = gameObject.GetComponent<Damager>();
            damager.OnAttack(player.gameObject.GetComponent<Damagee>());
        }
    }
    IEnumerator WaitForAlertAnim()
    {
        yield return new WaitForSeconds(.5f);
        anim.StopAlert();
    }
}
