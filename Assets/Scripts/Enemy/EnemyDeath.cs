using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] GameObject deadBody;

    public void onDeath()
    {
        GameObject db = Instantiate(deadBody, transform.position, transform.rotation);
        db.GetComponent<DeadEnemy>().setHost(GetComponent<EnemyStats>().getStats().type);
        Destroy(gameObject);
    }
}
