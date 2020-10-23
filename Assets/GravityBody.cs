using UnityEngine;

public class GravityBody : MonoBehaviour
{
    public Transform planet;

    public float maxGravity;
    public float maxGravityDistance;

    private Transform m_transform;
    private Rigidbody2D rb; // Player's Rigidbody2D

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_transform = transform;
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(planet.position, m_transform.position);
        Vector3 v = planet.position - m_transform.position;
        rb.AddForce(v.normalized * (1.0f - distance / maxGravityDistance) * maxGravity);
    }
}
