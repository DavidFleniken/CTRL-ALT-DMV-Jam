using UnityEngine;
using static GameManager;

public class DeadEnemy : MonoBehaviour
{
    [SerializeField] Host defaultHost = Host.Worm;
    Host host;

    private void Start()
    {
        if (defaultHost != Host.Worm)
        {
            host = defaultHost;
        }
    }

    public void setHost(Host host)
    {
        this.host = host;
    }

    public Host getHost()
    {
        return host;
    }
}
