using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("Combat Parameters")]
    [SerializeField] private float m_attackRange = 1.5f;
    [SerializeField] private float m_attackRate = 1f;
    [SerializeField] private int m_attackDamage = 10;
    [SerializeField] private LayerMask m_enemyLayer;

    [Header("References")]
    [SerializeField] private Transform m_attackPoint;
    [SerializeField] private GameObject m_weaponObject;

    private Animator m_animator;
    private Rigidbody m_body3d;
    private float m_nextAttackTime = 0f;
    private bool m_isAttacking = false;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body3d = GetComponent<Rigidbody>();
        m_weaponObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.time >= m_nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0)) // Left click for basic attack
            {
                Attack();
                m_nextAttackTime = Time.time + 1f / m_attackRate;
            }
        }
    }

    private void Attack()
    {
        // Trigger attack animation
        m_animator.SetTrigger("Attack");
        m_isAttacking = true;

        // Limit movement while attacking
        m_body3d.linearVelocity = new Vector3(0, m_body3d.linearVelocity.y, 0);
    }

    // Animation Event - Called during attack animation
    public void EnableWeapon()
    {
        m_weaponObject.SetActive(true);
    }

    // Animation Event - Called during attack animation
    public void DisableWeapon()
    {
        m_weaponObject.SetActive(false);
    }

    // Animation Event - Called at the point of impact in attack animation
    public void DoAttackDamage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(m_attackPoint.position, m_attackRange, m_enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(m_attackDamage);
                Debug.Log($"Hit {enemy.name} for {m_attackDamage} damage");
            }
        }

        m_isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (m_attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_attackPoint.position, m_attackRange);
    }
}
