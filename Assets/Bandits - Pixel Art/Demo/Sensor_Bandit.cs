using UnityEngine;
using System.Collections;

public class Sensor_Bandit : MonoBehaviour {

    private int m_ColCount = 0;
    private float m_DisableTimer;

    [Header("3D Sensor Settings")]
    [SerializeField] private LayerMask m_groundLayers = ~0; // Default to all layers
    [SerializeField] private bool m_checkTag = false;
    [SerializeField] private string m_groundTag = "Ground";

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;
    }

    void OnTriggerEnter(Collider other) // Changed from OnTriggerEnter2D
    {
        if (ShouldCountCollision(other))
        {
            m_ColCount++;
        }
    }

    void OnTriggerExit(Collider other) // Changed from OnTriggerExit2D
    {
        if (ShouldCountCollision(other))
        {
            m_ColCount--;
        }
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }

    private bool ShouldCountCollision(Collider other)
    {
        // Layer check
        if ((m_groundLayers.value & (1 << other.gameObject.layer)) == 0)
            return false;

        // Optional tag check
        if (m_checkTag && !other.CompareTag(m_groundTag))
            return false;

        return true;
    }
}
