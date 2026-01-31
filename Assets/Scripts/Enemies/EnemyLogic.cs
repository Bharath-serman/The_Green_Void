using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent navAgent;
    public Transform player;
    public Animator anim;
    public ParticleSystem hitEffect;

    [Header("Stats")]
    public float health = 100f;
    public int damage = 10;

    [Header("Movement/Detection")]
    public float walkPointRange = 10f;
    public float sightRange = 15f;
    public float attackRange = 2f;
    public float timeBetweenAttacks = 1.5f;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool alreadyAttacked;
    private bool takeDamage;
    private bool isDead;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;

        // Check if player is within ranges
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, LayerMask.GetMask("Player"));

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        else if (!playerInSightRange && takeDamage)
        {
            ChasePlayer();
        }
    }

    #region Patrol
    private void Patroling()
    {
        anim.SetBool("isWalking", true);

        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            navAgent.SetDestination(walkPoint);

        // Check if reached walk point
        if (Vector3.Distance(transform.position, walkPoint) < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 potentialPoint = transform.position + new Vector3(randomX, 0, randomZ);

        // Sample nearest point on NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(potentialPoint, out hit, 2f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }
    #endregion

    #region Chase
    private void ChasePlayer()
    {
        anim.SetBool("isWalking", true);
        navAgent.isStopped = false;
        navAgent.SetDestination(player.position);
    }
    #endregion

    #region Attack
    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position); // Stop moving
        navAgent.isStopped = true;
        anim.SetBool("isWalking", false);

        // Smooth rotation towards player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // Attack if not already attacking
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && !alreadyAttacked)
        {
            alreadyAttacked = true;

            // Random attack animation
            if (Random.value > 0.5f)
                anim.SetTrigger("Attack1");
            else
                anim.SetTrigger("Attack2");

            // Reset attack after cooldown
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    #endregion

    #region Damage & Death
    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;

        if (hitEffect != null)
            hitEffect.Play();

        StartCoroutine(TakeDamageCoroutine());

        if (health <= 0)
            Die();
    }

    private IEnumerator TakeDamageCoroutine()
    {
        takeDamage = true;
        yield return new WaitForSeconds(2f);
        takeDamage = false;
    }

    private void Die()
    {
        isDead = true;
        navAgent.isStopped = true;
        navAgent.enabled = false;

        anim.SetBool("isDead", true);
        Destroy(gameObject, 3f); // wait for death animation
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
