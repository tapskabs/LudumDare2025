using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { Wander, RushPlayer, Attack, TakeHit, Dead };

    [Header("AI & Movement")]
    public Transform player;
    public float wanderDistance = 10.0f;
    public float rushPlayerDistance = 5.0f;
    public float attackDistance = 1.0f;
    public float walkSpeed = 1.0f;
    public float runSpeed = 5.0f;
    public float attackRate = 1f;

    [Header("Enemy Stats")]
    public float maxHealth = 30.0f;
    public float damage = 10f;
    public float attackCooldown = 2f;

    private State currentState = State.Wander;
    private Vector3 wanderTarget;
    private float originalY;
    private float currentHealth;
    private float nextAttackTime;
    private Animator m_animator;

    private void Start()
    {
        currentHealth = maxHealth;
        wanderTarget = transform.position + new Vector3((Random.insideUnitCircle * wanderDistance).x, 0, (Random.insideUnitCircle * wanderDistance).y);
        originalY = transform.position.y;
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentState == State.Dead) return;

        switch (currentState)
        {
            case State.Wander:
                WanderBehavior();
                break;

            case State.RushPlayer:
                RushBehavior();
                break;

            case State.Attack:
                AttackBehavior();
                break;

            case State.TakeHit:
                // Handled in animation events
                break;
        }
    }

    private void WanderBehavior()
    {
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, walkSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, wanderTarget) < 1f)
        {
            wanderTarget = transform.position + new Vector3((Random.insideUnitCircle * wanderDistance).x, 0, (Random.insideUnitCircle * wanderDistance).y);
        }

        if (Vector3.Distance(transform.position, player.position) < rushPlayerDistance)
        {
            currentState = State.RushPlayer;
            m_animator.SetBool("Chasing", true);
        }
    }

    private void RushBehavior()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, runSpeed * Time.deltaTime);
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < attackDistance)
        {
            currentState = State.Attack;
            m_animator.SetBool("Chasing", false);
        }
        else if (distance > wanderDistance)
        {
            currentState = State.Wander;
            m_animator.SetBool("Chasing", false);
        }
    }

    private void AttackBehavior()
    {
        if (Time.time >= nextAttackTime)
        {
            m_animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackCooldown;
        }

        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            currentState = State.RushPlayer;
            m_animator.SetBool("Chasing", true);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentState == State.Dead) return;

        currentHealth -= damage;
        m_animator.SetTrigger("Hurt");
        currentState = State.TakeHit;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        currentState = State.Dead;
        m_animator.SetTrigger("Death");
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }

    // Animation Event - Called during attack animation
    public void OnEnemyAttack()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackDistance * 1.2f)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }

    // Animation Event - Called at the end of hit reaction animation
    public void OnHitRecoveryComplete()
    {
        if (currentHealth > 0)
        {
            currentState = State.RushPlayer;
            m_animator.SetBool("Chasing", true);
        }
    }
}
