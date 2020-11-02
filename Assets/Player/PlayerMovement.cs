using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D m_rigidBody;
    public Transform m_transform;
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();

        if (m_rigidBody == null)
            m_rigidBody = GetComponent<Rigidbody2D>();
        if (m_transform == null)
            m_transform = GetComponent<Transform>();
    }

    private void Start() => Jump(m_transform.up + m_transform.right * 3.75f, 1.75f);

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Jump.performed += _ => Jump((Vector2)m_transform.up + m_rigidBody.velocity.normalized, 0.55f);

    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump(Vector2 direction, float magnitude) => m_rigidBody.AddForce((direction) * magnitude, ForceMode2D.Impulse);
}
