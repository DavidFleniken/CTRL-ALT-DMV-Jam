using UnityEngine;
using static GameManager;

public class DeadEnemy : MonoBehaviour
{
    Host host;

    public void setHost(Host host)
    {
        this.host = host;
    }

    public Host getHost()
    {
        return host;
    }
}
