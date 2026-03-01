using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public enum Faction
    {
        Player,
        Enemy
    }

    [SerializeField] Faction parentFaction = Faction.Enemy;
    [SerializeField] float aliveDuration = 5f;
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;

    Attack parentAttack;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(aliveTimer());
    }

    public void setFaction(Faction newFaction)
    {
        parentFaction = newFaction;
    }

    public void setAttack(Attack attack)
    {
        parentAttack = attack;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }


        Stats colStats = null;

        if (parentFaction == Faction.Enemy)
        {
            if (col.CompareTag("Player"))
            {
                colStats = col.GetComponent<PlayerStats>();
            }
        }
        else
        {
            if (col.CompareTag("Enemy"))
            {
                colStats = col.GetComponent<EnemyStats>();
            }
        }
        if (hitSound != null)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(hitSound);
        }

        if (colStats != null)
        {
            colStats.applyAttack(parentAttack.getDamage());
            //Destroy(gameObject);
            // cant just destory since it messes with audio, instead have bullet "disapear" until deleted by aliveTimer
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }

    IEnumerator aliveTimer()
    {
        yield return new WaitForSeconds(aliveDuration);
        Destroy(gameObject);
    }
}
