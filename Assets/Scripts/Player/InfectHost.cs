using UnityEngine;
using static GameManager;
using System.Collections;

public class InfectHost : MonoBehaviour
{
    GameObject deadBody;
    [SerializeField] float infectCD = 1f;
    bool onCD = false;


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
        if (onCD)
            return;

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
        StartCoroutine(infectCooldown());
    }

    IEnumerator infectCooldown()
    {
        onCD = true;
        yield return new WaitForSeconds(infectCD);
        onCD = false;
    }
}
