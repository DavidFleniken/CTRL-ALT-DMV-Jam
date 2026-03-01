using UnityEngine;
using System.Collections;

public interface Attack
{
    public float getDamage();
}
public class PlayerAttack : MonoBehaviour, Attack
{
    private float damage;

    [SerializeField] float windupTime = 0.2f;
    [SerializeField] float attackUpTime = 0.1f;
    [SerializeField] float endingLag = 0.1f;
    [SerializeField] float knockback = 900f;
    [SerializeField] GameObject attackBox;

    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 5f;

    Rigidbody2D rb;
    PlayerMovement movement;

    [SerializeField] AudioClip playerAttackClip;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        attackBox.SetActive(false);
        movement = GetComponent<PlayerMovement>();
    }

    public void attack()
    {
        if (movement.getPaused())
        {
            return;
        }

        if (playerAttackClip != null)
        {
            audioSource.PlayOneShot(playerAttackClip);
        }

        damage = PlayerStats.getStats().damage;
        movement.pauseSecs(windupTime + attackUpTime + endingLag);
        StartCoroutine(doAttack());
    }

    IEnumerator doAttack()
    {
        attackDirection();
        yield return new WaitForSeconds(windupTime);
        
        if (PlayerStats.getStats().host != GameManager.Host.Cop)
        {
            attackBox.SetActive(true);
        }
        else
        {
            // cop unique logic
            GameObject shot = Instantiate(bullet, transform.position, attackBox.transform.rotation);
            Bullet b = shot.GetComponent<Bullet>();
            b.setFaction(Bullet.Faction.Player);
            b.setAttack(this);
            shot.GetComponent<Rigidbody2D>().linearVelocity = -(attackBox.transform.right).normalized * bulletSpeed;
            Debug.Log((attackBox.transform.right).normalized * bulletSpeed);
            //Debug.Log(shot.GetComponent<Rigidbody2D>().linearVelocity);
        }
        yield return new WaitForSeconds(attackUpTime);
        attackBox.SetActive(false);
    }

    public void attackDirection()
    {
        Vector2 dir = movement.getDir();

        // snap to nearest 90 degree increment
        Vector2 snapped;

        if (dir == Vector2.zero || PlayerStats.getStats().host == GameManager.Host.Cop)
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
        return PlayerStats.getStats().damage;

    }

    public float getKnockback()
    {
        return knockback;
    }
}
