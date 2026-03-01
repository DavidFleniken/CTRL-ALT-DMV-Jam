using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    bool hitThisAttack = false;
    static int totalhits = 0;
    [SerializeField] AudioClip hitSound;
    AudioSource hitAudioSource;

    private void OnEnable()
    {
        hitAudioSource = GetComponent<AudioSource>();
        hitThisAttack = false;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        //Debug.Log("Hit " + col.name + " and HTA = " + hitThisAttack + " and TH = " + totalhits);
        if (hitThisAttack)
            return;
        //Debug.Log("Tag: " + col.gameObject.tag);
        if (col.gameObject.CompareTag("Player"))
        {
            EnemyAttack attack = gameObject.GetComponentInParent<EnemyAttack>();

            if (attack == null)
            {
                Debug.LogError("Couldn't find enemy attack");
            }
            else
            {
                hitThisAttack = true;
                totalhits++;
                PlayerObject.getPlayer().GetComponent<PlayerStats>().applyAttack(attack.getDamage(), attack.getKnockback());
                if (hitSound != null)
                {
                    hitAudioSource.PlayOneShot(hitSound);
                }
            }
        }
    }
}
