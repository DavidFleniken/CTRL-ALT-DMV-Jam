using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Handles translation of inputs into player movement

    [SerializeField] float speed = 5f;

    Rigidbody2D rb;
    Vector2 velo;
    Vector2 lastDir;

    bool paused = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        velo = context.ReadValue<Vector2>() * speed;

        if (velo != Vector2.zero)
        {
            lastDir = velo.normalized;
        }
    }

    private void Update()
    {
        if (paused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // simulate friction - mainly meant for knockback stuff - need to make sure it doesn't mess up normal movement
        float diff = rb.linearVelocity.magnitude - velo.magnitude;
        if (diff > 0f)
        {
            if (diff < speed) // When almost done, snap
            {
                rb.linearVelocity = velo;
            }
            else
            {
                rb.linearVelocity -= rb.linearVelocity * 10f * Time.deltaTime;
            }
        }
        else
        {
            rb.linearVelocity = velo;
        }

    }

    // Stops enemy movement for "secs" seconds
    public void pauseSecs(float secs)
    {
        Debug.Log("Paused for: " + secs);
        paused = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(PauseFor(secs));
    }

    IEnumerator PauseFor(float secs)
    {
        yield return new WaitForSeconds(secs);
        paused = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public Vector2 getDir()
    {
        return lastDir;
    }

    public bool getPaused()
    {
        return paused;
    }
}
