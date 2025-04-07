using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public float attackCooldown = 0.5f;

    private PlayerStats playerStats;
    private PlayerAnimatorController animController;
    private float lastAttackTime;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        animController = GetComponent<PlayerAnimatorController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime)
        {
            Attack();
            lastAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        animController.PlayAttack();

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemyCollider in hitEnemies)
        {
            EnemyStats enemy = enemyCollider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                int damage = playerStats.GetAttackDamage();
                enemy.TakeDamage(damage);
                Debug.Log($"[Player] Dealt {damage} to {enemy.name}");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}