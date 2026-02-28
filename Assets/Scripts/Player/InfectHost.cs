using UnityEngine;
using static GameManager;

public class InfectHost : MonoBehaviour
{
    GameObject deadBody;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Dead"))
        {
            deadBody = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead"))
        {
            deadBody = null;
        }
    }

    public void infect()
    {
        Host curHost = PlayerStats.getStats().host;

        if (deadBody != null)
        {
            // infect new body
            PlayerStats.changeHost(deadBody.GetComponent<DeadEnemy>().getHost());
            Destroy(deadBody);
        }
        else if (curHost != Host.Worm)
        {
            // become worm (if not already)
            PlayerStats.changeHost(Host.Worm);
        }
    }
}
