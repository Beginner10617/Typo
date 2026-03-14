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
    public float enemySpeed = 1.7f;
    public State currentState;

    Animator animator;
    public Transform player;
    bool canReach = false;

    public int damage = 10;

    public float detectionRange = 12f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private float lastAttackTime;

    private NavMeshAgent agent;

    // --- Animation rotation offsets (edit in Inspector) ---
    [Header("Animation Rotation Offsets")]
    public float idleYawOffset = 0f;
    public float walkYawOffset = 0f;
    public float runYawOffset = 0f;
    public float attackYawOffset = 0f;


    [Header("Sounds")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;
    public float attackHitDelay = 0.35f; // adjust to sync with animation frame

    // Helper function
    bool HasPathToPlayer()
    {
        NavMeshPath path = new NavMeshPath();

        if(agent.CalculatePath(player.position, path))
        {
            return path.status == NavMeshPathStatus.PathComplete;
        }

        return false;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Idle;
        animator = GetComponent<Animator>();
    }

    float pathCheckTimer;

    void Update()
    {
        pathCheckTimer += Time.deltaTime;

        if(pathCheckTimer >= 0.5f)
        {
            canReach = HasPathToPlayer();
            pathCheckTimer = 0f;
        }

        if(currentState == State.Dead)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        // Debug.Log("Enemy State: " + currentState + " | Distance to Player: " + distance.ToString("F2") + " | Can Reach: " + canReach);
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

    void RotateTowardsPlayer(float yawOffset)
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;

        if(dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            targetRot *= Quaternion.Euler(0, yawOffset, 0);
            transform.rotation = targetRot;
        }
    }

    void RotateTowardsDirection(Vector3 direction, float yawOffset)
    {
        direction.y = 0;

        if(direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRot = Quaternion.LookRotation(direction.normalized);
        targetRot *= Quaternion.Euler(0, yawOffset, 0);

        transform.rotation = targetRot;
    }

    void IdleState(float distance)
    {
        agent.isStopped = true;

        animator.SetFloat("Speed", 0f);

        RotateTowardsDirection(player.position - transform.position, idleYawOffset);

        if(distance < detectionRange)
        {
            currentState = State.Chase;
        }
    }

    void ChaseState(float distance)
    {
        if(!canReach)
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0f);
            RotateTowardsPlayer(idleYawOffset);
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);
        RotateTowardsDirection(agent.steeringTarget - transform.position, walkYawOffset);
        
        agent.speed = enemySpeed;
        animator.SetFloat("Speed", 0.6f);
        
        if(audioSource != null && walkSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = walkSound;
            audioSource.loop = false;
            audioSource.Play();
        }

        if(distance < attackRange)
        {
            currentState = State.Attack;
        }
    }

    void AttackState(float distance)
    {
        agent.isStopped = true;

        RotateTowardsPlayer(attackYawOffset);

        animator.SetFloat("Speed", 0f);

        if(Time.time > lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("Attack");

            // delay the hit + sound to match animation frame
            Invoke(nameof(Attack), attackHitDelay);

            lastAttackTime = Time.time;
        }

        if(distance > attackRange)
        {
            currentState = State.Chase;
            animator.ResetTrigger("Attack");
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacks player");
        player.GetComponent<ThirdPersonShooterController>().TakeDamage(damage);

        if(audioSource != null && attackSound != null)
        {
            audioSource.clip = attackSound;
            audioSource.loop = false;
            audioSource.Play();
        }
    }


    public void Die()
    {
        currentState = State.Dead;

        agent.isStopped = true;
        if(audioSource != null && deathSound != null)
        {
            audioSource.clip = deathSound;
            audioSource.loop = false;
            audioSource.Play();
        }
        animator.SetBool("Dead", true);

        Destroy(gameObject, 3f);
    }
}