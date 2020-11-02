using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            Debug.Log("Collided with the player");
            Destroy(gameObject);
        }
        else if (collision.TryGetComponent<GravityBody>(out GravityBody planet))
        {
            // Reduce planet health
            Destroy(gameObject);
        }
    }
}
