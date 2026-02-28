using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    private float damage;
    private EnemyStats stats;
    private EnemyMovement movement;

    [SerializeField] float windupTime = 0.2f;
    [SerializeField] float attackUpTime = 0.1f;
    [SerializeField] float endingLag = 0.1f;
    [SerializeField] GameObject attackBox;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        stats = GetComponent<EnemyStats>();
        damage = stats.getStats().damage;

        attackBox.SetActive(false);
    }

    public void attack()
    {
        movement.pauseSecs(windupTime + attackUpTime + endingLag);
        StartCoroutine(doAttack());
    }

    IEnumerator doAttack()
    {
        yield return new WaitForSeconds(windupTime);
        attackDirection();
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackUpTime);
        attackBox.SetActive(false);
    }

    public void attackDirection()
    {
        Vector2 playerPos = PlayerObject.getPlayer().transform.position;
        Vector2 dir = (playerPos - (Vector2)transform.position);

        // snap to nearest 90 degree increment
        Vector2 snapped;

        if (dir == Vector2.zero)
        {
            snapped = dir;
        }
        else
        {
            if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
                snapped = new Vector2(Mathf.Sign(dir.x), 0f);
            else
                snapped = new Vector2(0f, Mathf.Sign(dir.y));

        }

        float angle = Mathf.Atan2(snapped.y, snapped.x) * Mathf.Rad2Deg;
        attackBox.transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f); // sub by 180 cause I messed up something somewhere I guess
    }

    public float getDamage()
    {
        return damage;
    }
}
