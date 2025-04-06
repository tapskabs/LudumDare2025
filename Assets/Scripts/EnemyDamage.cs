using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f;
    private float lastDamageTime;

    private void OnTriggerEnter(Collider other)
    {
        TryDamagePlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryDamagePlayer(other);
    }

    private void TryDamagePlayer(Collider other)
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return;

        PlayerStats player = other.GetComponent<PlayerStats>();
        if (player != null)
        {
            Debug.Log($"[Enemy] Attacking player for {damage} damage.");
            player.TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }
}
