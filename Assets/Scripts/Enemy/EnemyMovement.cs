using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    // Moves in a straight line to player at set speed

    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 2f;
    
    Rigidbody2D rb;
    EnemyAttack attack;

    bool paused = false;

    private void Start()
    {
        attack = GetComponent<EnemyAttack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (paused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 playerPos = PlayerObject.getPlayer().transform.position;
        Vector2 moveDir = (playerPos - (Vector2)transform.position);

        if (moveDir.magnitude <= attackRange)
        {
            attack.attack();
            return;
        }

        rb.linearVelocity = moveDir.normalized * speed;
    }

    // Stops enemy movement for "secs" seconds
    public void pauseSecs(float secs)
    {
        Debug.Log("Paused for: " + secs);
        paused = true;
        StartCoroutine(PauseFor(secs));
    }

    IEnumerator PauseFor(float secs)
    {
        yield return new WaitForSeconds(secs);
        paused = false;
    }

}
