using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float currentHealth;

    [Header("Visual Feedback")]
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private AudioClip hitSound;

    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Visual/Audio feedback
        if (hitEffectPrefab) Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        if (hitSound) AudioSource.PlayClipAtPoint(hitSound, transform.position);

        animator?.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator?.SetTrigger("Death");
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2f); // Delay to allow death animation
    }
}
