using UnityEngine;

public class FitToCamera : MonoBehaviour
{
    private const int offShootWidth = 2;
    private const int offShootHeight = 2;
    public Transform m_transform;

    private void Awake()
    {
        if (m_transform == null)
            m_transform = transform;
    }

    private void Update()
    {
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        m_transform.localScale = new Vector3(screenWidth * offShootWidth, screenHeight * offShootHeight, m_transform.localScale.z);
    }
}
