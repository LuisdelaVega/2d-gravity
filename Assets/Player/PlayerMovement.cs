using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D m_rigidBody;
    public Transform m_transform;
    private Controls controls;

    //[SerializeField] private float speed = 2;

    private void Awake()
    {
        controls = new Controls();

        if (m_rigidBody == null)
            m_rigidBody = GetComponent<Rigidbody2D>();
        if (m_transform == null)
            m_transform = GetComponent<Transform>();
    }

    private void Start() => m_rigidBody.AddForce(Vector2.right, ForceMode2D.Impulse);

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Jump.performed += _ => m_rigidBody.AddForce((m_transform.up + m_transform.right * 1.5f), ForceMode2D.Impulse);
        //controls.Player.Land.performed += _ => m_rigidBody.AddForce((-m_transform.up), ForceMode2D.Impulse);
    }
    private void OnDisable() => controls.Disable();
}
