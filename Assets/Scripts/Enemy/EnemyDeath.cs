using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] GameObject deadBody;
    [SerializeField] AudioClip deathSound;
    AudioSource audioSource;

    public void onDeath()
    {
        audioSource = GetComponent<AudioSource>();
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        GameObject db = Instantiate(deadBody, transform.position, transform.rotation);
        db.GetComponent<DeadEnemy>().setHost(GetComponent<EnemyStats>().getStats().type);
        Destroy(gameObject);
    }
}
