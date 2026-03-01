using UnityEngine;
using System.Collections;
using static EnemyStats;

public class EnemyMovement : MonoBehaviour
{
    // Moves in a straight line to player at set speed

    // Each enemy type get's their own array of animation clips
    // by index: 0 is idle, 1 is walk, 2 is attack
    [SerializeField] AnimationClip[] adultClips;


    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float copRangeModifier = 5f;
    
    Rigidbody2D rb;
    EnemyAttack attack;

    bool paused = false;
    bool stuckChecking = false;
    float maxTimeStuck = 2f; // secs before they get deleted
    float stuckThreshold = 0.1f; // maximum speed to be considered stuck
    float stuckCheckStart;

    Vector2 lastPos = Vector2.zero;

    private void Start()
    {
        attack = GetComponent<EnemyAttack>();
        rb = GetComponent<Rigidbody2D>();

        NPCStats stats = GetComponent<EnemyStats>().getStats();

        if (stats.type == GameManager.Host.Cop)
        {
            // increase attack range by a lot
            attackRange *= copRangeModifier;
        }

        speed = stats.speed;
    }

    private void FixedUpdate()
    {
        // check if stuck
        //Debug.Log("velo: " + ((Vector2)transform.position - lastPos).magnitude / Time.deltaTime);

        if (stuckChecking)
        {
            if (((Vector2)transform.position - lastPos).magnitude / Time.deltaTime > stuckThreshold)
            {
                stuckChecking = false;
            }
            else if (Time.time > maxTimeStuck + stuckCheckStart)
            {
                // detected stuck enemy
                Destroy(gameObject);
            }

        }
        else if (((Vector2)transform.position - lastPos).magnitude / Time.deltaTime < stuckThreshold)
        {
            stuckCheckStart = Time.time;
            stuckChecking = true;
        }

        lastPos = transform.position;
    }

    private void Update()
    {
        if (paused)
        {
            rb.linearVelocity = Vector2.zero;

            // reset any stuck checking for good measure
            stuckChecking = false;

            return;
        }

        

        NPCStats stats = GetComponent<EnemyStats>().getStats();

        speed = stats.speed;

        Vector2 playerPos = PlayerObject.getPlayer().transform.position;
        Vector2 moveDir = (playerPos - (Vector2)transform.position);

        if (moveDir.magnitude <= attackRange)
        {
            attack.attack();
            return;
        }

        Vector2 velo = moveDir.normalized * speed;

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
        //Debug.Log("Paused for: " + secs);
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

}
