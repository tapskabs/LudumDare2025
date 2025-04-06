using UnityEngine;

public class EnemyFacing : MonoBehaviour
{
    public Transform player; // Assign in Inspector or find via tag
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // Assumes child holds the sprite

        if (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found) player = found.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;

        // Flip sprite based on horizontal direction
        if (direction.x > 0)
            spriteRenderer.flipX = false; // Facing right
        else if (direction.x < 0)
            spriteRenderer.flipX = true; // Facing left
    }
}
