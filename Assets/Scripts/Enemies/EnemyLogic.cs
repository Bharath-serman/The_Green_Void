using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyLogic : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent navAgent;
    public Transform player;
    public Animator anim;

    [Header("Stats")]
    public float health = 100f;

    [Header("Movement / Detection")]
    public float walkPointRange = 10f;
    public float sightRange = 15f;
    public float attackRange = 2f;
    public float timeBetweenAttacks = 1.5f;
    public float attackCommitTime = 0.2f;

    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool isAttacking;
    private bool attackCommitted;
    private bool isDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (ChaosManager.Instance != null)
        {
            ChaosManager.Instance.RegisterEnemy(this);
        }


        navAgent.updatePosition = true;
        navAgent.updateRotation = true;
    }

    private void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 🔒 While attacking, don't change state
        if (isAttacking)
            return;

        if (distance <= attackRange)
        {
            StartAttack();
        }
        else if (!attackCommitted && distance <= sightRange)
        {
            ChasePlayer();
        }
        else if (!attackCommitted)
        {
            Patrol();
        }
    }

    // ===================== PATROL =====================
    private void Patrol()
    {
        anim.SetBool("isWalking", true);
        navAgent.isStopped = false;

        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            navAgent.SetDestination(walkPoint);

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint()
    {
        Vector3 randomPoint = transform.position +
            new Vector3(Random.Range(-walkPointRange, walkPointRange), 0,
                        Random.Range(-walkPointRange, walkPointRange));

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    // ===================== CHASE =====================
    private void ChasePlayer()
    {
        anim.SetBool("isWalking", true);
        navAgent.isStopped = false;
        navAgent.SetDestination(player.position);
    }

    // ===================== ATTACK =====================
    private void StartAttack()
    {
        isAttacking = true;

        navAgent.isStopped = true;
        navAgent.ResetPath();
        navAgent.velocity = Vector3.zero;

        anim.SetBool("isWalking", false);

        Vector3 dir = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        anim.ResetTrigger("Attack1");
        anim.ResetTrigger("Attack2");

        if (Random.value > 0.5f)
            anim.SetTrigger("Attack1");
        else
            anim.SetTrigger("Attack2");

        Invoke(nameof(EndAttack), timeBetweenAttacks);
    }

    private void EndAttack()
    {
        isAttacking = false;
        StartCoroutine(AttackCommitCoroutine());
    }

    private IEnumerator AttackCommitCoroutine()
    {
        attackCommitted = true;

        navAgent.isStopped = true;
        navAgent.velocity = Vector3.zero;

        yield return new WaitForSeconds(attackCommitTime);

        attackCommitted = false;
        navAgent.isStopped = false;
    }

    // ===================== DEATH =====================
    private void Die()
    {
        isDead = true;
        navAgent.isStopped = true;
        navAgent.enabled = false;
        anim.SetBool("isDead", true);
    }
}
