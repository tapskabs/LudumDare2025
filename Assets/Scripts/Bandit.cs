using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rotationSpeed = 10f; // Added for smooth rotation

    private Animator m_animator;
    private Rigidbody m_body3d; // Changed to 3D Rigidbody
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body3d = GetComponent<Rigidbody>(); // Changed to 3D Rigidbody
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        // Lock rigidbody rotation if needed
        m_body3d.freezeRotation = true;
    }

    void Update()
    {
        // Ground check remains similar
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle 3D movement input --
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * inputZ + cameraRight * inputX).normalized;

        // Move character
        if (moveDirection != Vector3.zero)
        {
            // Smooth rotation towards movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_rotationSpeed * Time.deltaTime);

            // Move forward
            m_body3d.linearVelocity = new Vector3(
                moveDirection.x * m_speed,
                m_body3d.linearVelocity.y,
                moveDirection.z * m_speed
            );

            m_animator.SetInteger("AnimState", 2); // Run
        }
        else
        {
            m_body3d.linearVelocity = new Vector3(0, m_body3d.linearVelocity.y, 0);
            m_animator.SetInteger("AnimState", m_combatIdle ? 1 : 0); // Combat Idle or Idle
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body3d.linearVelocity.y);

        // -- Handle Animations (remain similar) --
        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");
            m_isDead = !m_isDead;
        }
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");
        else if (Input.GetMouseButtonDown(0))
            m_animator.SetTrigger("Attack");
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body3d.linearVelocity = new Vector3(m_body3d.linearVelocity.x, m_jumpForce, m_body3d.linearVelocity.z);
            m_groundSensor.Disable(0.2f);
        }
    }
}
