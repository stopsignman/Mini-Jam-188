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

    public float walkSpeed = 6;
    public float sprintSpeed = 12;
    public AudioClip[] enemySounds;
    private AudioSource audioSource;
    public bool randomSound = true;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<EnemyAnimator>();
        audioSource = GetComponent<AudioSource>();
        if (randomSound)
        {
            StartCoroutine(PlayRandomSound());
        }
        else
        {
            audioSource.loop = true;
            audioSource.clip = enemySounds[0];
            audioSource.Play();
        }
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
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        anim.OnWander();
        agent.speed = walkSpeed;
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
        agent.speed = sprintSpeed;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            anim.OnAttack();
            StartCoroutine(WaitForAnimation());
            alreadyAttacked = true;
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
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
    IEnumerator WaitForAlertAnim()
    {
        yield return new WaitForSeconds(.5f);
        anim.StopAlert();
    }

    IEnumerator PlayRandomSound()
    {
        yield return new WaitForSeconds(Random.Range(2, 6));
        audioSource.PlayOneShot(enemySounds[Random.Range(0, enemySounds.Length - 1)], 2);
        StartCoroutine(PlayRandomSound());
    }
}
