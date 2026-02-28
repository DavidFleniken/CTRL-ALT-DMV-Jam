using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Handles translation of inputs into player movement

    [SerializeField] float speed = 5f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 velo = context.ReadValue<Vector2>() * speed;
        rb.linearVelocity = velo;
    }
}
