using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Attack,
        Dead
    }

    public State currentState;

    public Transform player;

    public float detectionRange = 12f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private float lastAttackTime;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Idle;
    }

    void Update()
    {
        if(currentState == State.Dead)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch(currentState)
        {
            case State.Idle:
                IdleState(distance);
                break;

            case State.Chase:
                ChaseState(distance);
                break;

            case State.Attack:
                AttackState(distance);
                break;
        }
    }

    void IdleState(float distance)
    {
        agent.isStopped = true;

        if(distance < detectionRange)
        {
            currentState = State.Chase;
        }
    }

    void ChaseState(float distance)
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if(distance < attackRange)
        {
            currentState = State.Attack;
        }
        else if(distance > detectionRange * 1.5f)
        {
            currentState = State.Idle;
        }
    }

    void AttackState(float distance)
    {
        agent.isStopped = true;

        transform.LookAt(player);

        if(Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }

        if(distance > attackRange)
        {
            currentState = State.Chase;
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacks player");
        // player.TakeDamage();
    }

    public void Die()
    {
        currentState = State.Dead;
        agent.isStopped = true;

        Destroy(gameObject, 2f);
    }
}