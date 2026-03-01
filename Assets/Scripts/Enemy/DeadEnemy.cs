using UnityEngine;
using static GameManager;

public class DeadEnemy : MonoBehaviour
{
    [SerializeField] Host defaultHost = Host.Worm;
    [SerializeField] Sprite[] deadSprites;
    Host host;
    Sprite deadSprite;

    private void Start()
    {
        if (defaultHost != Host.Worm)
        {
            host = defaultHost;
        }

        setSprite();
    }

    void setSprite()
    {
        switch (host)
        {
            case Host.Worm:
                
                break;

            case Host.Cat:
                deadSprite = deadSprites[0];
                break;

            case Host.Dog:
                deadSprite = deadSprites[1];
                break;

            case Host.Child:
                deadSprite = deadSprites[2];
                break;

            case Host.Adult:
                deadSprite = deadSprites[3];
                break;

            case Host.Cop:
                deadSprite = deadSprites[4];
                break;
        }

        GetComponent<SpriteRenderer>().sprite = deadSprite;

    }

    public void setHost(Host host)
    {
        this.host = host;
        setSprite();
    }

    public Host getHost()
    {
        return host;
    }
}
