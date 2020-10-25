using System;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    [Header("Planet")]
    [SerializeField] private float maxGravity = 10;
    [SerializeField] private float maxGravityDistance = 50;
    public Transform m_transform;

    [Header("Player")]
    public Transform playerTransform;
    public Rigidbody2D playerRB;
    private float angle;
    private readonly float playerRotationSpeed = 100f;

    [Header("Line Renderer")]
    public LineRenderer lineRenderer;
    public int vertexCount = 40; // 4 vertices == square
    public float lineWidth = 0.2f;

    public static event Action<Transform> onEnteredGravity;

    private void Awake()
    {
        if (m_transform == null)
            m_transform = transform;
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        SetupCircle();
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(m_transform.position, playerTransform.position);

        if (distance > maxGravityDistance) return;
        onEnteredGravity?.Invoke(m_transform);

        Vector3 v = m_transform.position - playerTransform.position;
        playerRB.AddForce(v.normalized * (1.0f - distance / maxGravityDistance) * maxGravity);

        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, Quaternion.AngleAxis(angle + 90, Vector3.forward), playerRotationSpeed * Time.deltaTime);
    }

    private void SetupCircle()
    {
        lineRenderer.widthMultiplier = lineWidth;

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        lineRenderer.positionCount = vertexCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(m_transform.position.x + maxGravityDistance * Mathf.Cos(theta), m_transform.position.y + maxGravityDistance * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        Vector3 oldPos = m_transform.position;
        for (int i = 0; i < vertexCount + 1; i++)
        {
            Vector3 pos = new Vector3(maxGravityDistance * Mathf.Cos(theta), maxGravityDistance * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, m_transform.position + pos);
            oldPos = m_transform.position + pos;

            theta += deltaTheta;
        }
    }
#endif
}
