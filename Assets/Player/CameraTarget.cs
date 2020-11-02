using Cinemachine;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [Header("Camera Target")]
    public Transform m_transform;
    [SerializeField] private float m_interpolant = 0.15f;

    [Header("Planet")]
    public Transform planetTransform;

    [Header("Virtual Camera")]
    public CinemachineVirtualCamera vcam;
    [SerializeField] private float maxOrthographicSize = 30f;
    private float minOrthographicSize = 3.5f;
    [SerializeField] private float vcamInterpolant = 2f;

    private void Awake()
    {
        if (m_transform == null)
            m_transform = transform;

        minOrthographicSize = vcam.m_Lens.OrthographicSize;
    }

    private void OnEnable() => GravityBody.onEnteredGravity += SwitchPlanet;
    private void OnDisable() => GravityBody.onEnteredGravity -= SwitchPlanet;

    private void Update()
    {
        m_transform.position = Vector3.Lerp(m_transform.parent.position, planetTransform.position, m_interpolant);
        // Adjust the distance of the camera to the player (Expand the Field of View)
        vcam.m_Lens.OrthographicSize = Mathf.Min(minOrthographicSize + (m_transform.parent.position - planetTransform.position).sqrMagnitude / vcamInterpolant, maxOrthographicSize);
    }

    private void SwitchPlanet(Transform newPlanetTransform) => planetTransform = newPlanetTransform;
}
