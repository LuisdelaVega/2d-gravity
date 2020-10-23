using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Controls controls;
    private Rigidbody2D rb;
    private Vector2 direction;

    [SerializeField] private float speed;

    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Movement.performed += ctx => SetDirection(ctx.ReadValue<Vector2>());
    }
    private void OnDisable() => controls.Disable();

    private void FixedUpdate()
    {
        if (direction.sqrMagnitude != 0)
            rb.AddForce(speed * direction);
    }

    private void SetDirection(Vector2 dir) => direction = dir;
}
