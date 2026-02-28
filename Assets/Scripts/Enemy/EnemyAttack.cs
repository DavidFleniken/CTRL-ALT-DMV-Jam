using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour, Attack
{
    private float damage;
    private EnemyStats stats;
    private EnemyMovement movement;

    [SerializeField] float windupTime = 0.2f;
    [SerializeField] float attackUpTime = 0.1f;
    [SerializeField] float endingLag = 0.1f;
    [SerializeField] float knockback = 900f;
    [SerializeField] GameObject attackBox;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float copLagMult = 2.5f;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        stats = GetComponent<EnemyStats>();
        damage = stats.getStats().damage;

        attackBox.SetActive(false);

        // set changes for cop
        if (stats.getStats().type == GameManager.Host.Cop)
        {
            windupTime *= copLagMult;
            endingLag *= copLagMult;
        }
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

        if (stats.getStats().type != GameManager.Host.Cop)
        {
            attackBox.SetActive(true);
        }
        else
        {
            // cop unique logic
            GameObject shot = Instantiate(bullet, transform.position, attackBox.transform.rotation);
            Bullet b = shot.GetComponent<Bullet>();
            b.setFaction(Bullet.Faction.Enemy);
            b.setAttack(this);
            shot.GetComponent<Rigidbody2D>().linearVelocity = -(attackBox.transform.right).normalized * bulletSpeed;
            //Debug.Log((attackBox.transform.rotation.eulerAngles).normalized * bulletSpeed);
            //Debug.Log(shot.GetComponent<Rigidbody2D>().linearVelocity);
        }


        yield return new WaitForSeconds(attackUpTime);
        attackBox.SetActive(false);
    }

    public void attackDirection()
    {
        Vector2 playerPos = PlayerObject.getPlayer().transform.position;
        Vector2 dir = (playerPos - (Vector2)transform.position);

        // snap to nearest 90 degree increment unless cop
        Vector2 snapped;

        if (stats.getStats().type != GameManager.Host.Cop)
        {
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
        }
        else
        {
            snapped = dir;
        }

        

        float angle = Mathf.Atan2(snapped.y, snapped.x) * Mathf.Rad2Deg;
        attackBox.transform.rotation = Quaternion.Euler(0f, 0f, angle - 180f); // sub by 180 cause I messed up something somewhere I guess
    }

    public float getDamage()
    {
        return stats.getStats().damage;
    }

    public Vector2 getKnockback()
    {
        Vector2 playerPos = PlayerObject.getPlayer().transform.position;
        Vector2 dir = (playerPos - (Vector2)transform.position).normalized;

        return dir * knockback;
    }
}
