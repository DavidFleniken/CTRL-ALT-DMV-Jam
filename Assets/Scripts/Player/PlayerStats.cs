using UnityEngine;
using static GameManager;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    // Stores Player Stats (Health, Damage, etc) and provides tools for modifying those values

    [SerializeField] float defaultHealth = 1f;
    [SerializeField] float defaultSpeed = 3f;
    [SerializeField] float defaultDamage = 0f;
    [SerializeField] Host defaultHost = Host.Worm;
    [SerializeField] TMP_Text hpUI;

    static float curHealth;
    static float curSpeed;
    static float curDamage;
    static Host curHost;

    const float hpDrain = 1f; // amount of hp lost every second

    Rigidbody2D rb;

    private void Start()
    {
        curHealth = defaultHealth;
        curSpeed = defaultSpeed;
        curDamage = defaultDamage;
        curHost = defaultHost;

        rb = GetComponent<Rigidbody2D>();
    }

    public struct stats
    {
        public float health;
        public float speed;
        public float damage;
        public Host host;

        public stats(float curHealth, float curSpeed, float curDamage, Host curHost)
        {
            health = curHealth;
            speed = curSpeed;
            damage = curDamage;
            host = curHost;
        }
    }

    public static void changeHost(Host newHost)
    {
        //Debug.Log("Changed Host to: " + newHost);
        curHost = newHost;

        switch (newHost)
        {
            case Host.Worm:
                curHealth = 1;
                curSpeed = 3;
                curDamage = 0;
                break;

            case Host.Cat:
                curHealth = 10;
                curSpeed = 5;
                curDamage = 4;
                break;

            case Host.Dog:
                curHealth = 15;
                curSpeed = 4;
                curDamage = 8;
                break;

            case Host.Child:
                curHealth = 20;
                curSpeed = 3;
                curDamage = 10;
                break;

            case Host.Adult:
                curHealth = 30;
                curSpeed = 2;
                curDamage = 15;
                break;

            case Host.Cop:
                curHealth = 30;
                curSpeed = 2;
                curDamage = 30;
                break;
        }
    }

    private void Update()
    {
        if (curHost != Host.Worm)
            curHealth -= hpDrain * Time.deltaTime;

        hpUI.text = "HP: " + ((int)curHealth + 1);
        //Debug.Log(getStats().host);

        if (curHealth + 1 <= 0)
        {
            // Do game over stuff
        }
    }

    public static stats getStats()
    {
        return new stats(curHealth, curSpeed, curDamage, curHost);
    }

    public void applyAttack(float damage, Vector2 knockback)
    {
        Debug.Log("before hit: hp = " + curHealth + " damage = " + damage);
        curHealth -= damage;
        rb.AddForce(knockback);

        Debug.Log("Hit: hp = " + curHealth);
    }
}
