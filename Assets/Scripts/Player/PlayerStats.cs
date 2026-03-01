using UnityEngine;
using static GameManager;
using TMPro;
using UnityEngine.UI;

public interface Stats
{
    public void applyAttack(float damage);
}
public class PlayerStats : MonoBehaviour, Stats
{
    // Stores Player Stats (Health, Damage, etc) and provides tools for modifying those values

    [SerializeField] float defaultHealth = 1f;
    [SerializeField] float defaultSpeed = 3f;
    [SerializeField] float defaultDamage = 0f;
    [SerializeField] Host defaultHost = Host.Worm;
    [SerializeField] Image healthBar;

    static float curHealth;
    static float maxHealth;
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
                curSpeed = 1.5f;
                curDamage = 0;
                PlayerObject.getPlayer().GetComponent<SpriteRenderer>().color = Color.white;
                PlayerObject.getPlayer().GetComponent<Animator>().SetTrigger("isWorm");
                PlayerObject.getPlayer().transform.localScale = new Vector3(0.23f, 0.23f, 0.23f);
                break;

            case Host.Cat:
                curHealth = 10;
                curSpeed = 5;
                curDamage = 4;
                break;

            case Host.Dog:
                curHealth = 15;
                curSpeed = 8;
                curDamage = 8;
                PlayerObject.getPlayer().GetComponent<Animator>().SetTrigger("isDog");
                PlayerObject.getPlayer().transform.rotation = Quaternion.identity;
                PlayerObject.getPlayer().transform.localScale = new Vector3(0.73f, 0.73f, 0.73f);
                break;

            case Host.Child:
                curHealth = 20;
                curSpeed = 9;
                curDamage = 3;
                PlayerObject.getPlayer().GetComponent<Animator>().SetTrigger("isChild");
                PlayerObject.getPlayer().transform.rotation = Quaternion.identity;
                PlayerObject.getPlayer().transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                break;

            case Host.Adult:
                curHealth = 30;
                curSpeed = 5;
                curDamage = 6;
                PlayerObject.getPlayer().GetComponent<Animator>().SetTrigger("isAdult");
                PlayerObject.getPlayer().transform.rotation = Quaternion.identity;
                PlayerObject.getPlayer().transform.localScale = new Vector3(0.73f, 0.73f, 0.73f);
                break;

            case Host.Cop:
                curHealth = 30;
                curSpeed = 5;
                curDamage = 15;
                PlayerObject.getPlayer().GetComponent<Animator>().SetTrigger("isCop");
                PlayerObject.getPlayer().transform.rotation = Quaternion.identity;
                PlayerObject.getPlayer().transform.localScale = new Vector3(0.73f, 0.73f, 0.73f);
                break;
        }

        maxHealth = curHealth;
    }

    private void Update()
    {
        if (curHost != Host.Worm)
            curHealth -= hpDrain * Time.deltaTime;

        healthBar.fillAmount = (curHealth / maxHealth);
        //Debug.Log(getStats().host);

        if (curHealth + 1 <= 0)
        {
            if (curHost == Host.Worm)
                GetComponent<PlayerDeath>().onDeath();
            else
            {
                GetComponent<InfectHost>().infect();
            }
        }
    }

    public static stats getStats()
    {
        return new stats(curHealth, curSpeed, curDamage, curHost);
    }

    public void applyAttack(float damage, Vector2 knockback)
    {
        //Debug.Log("before hit: hp = " + curHealth + " damage = " + damage);
        curHealth -= damage;
        rb.AddForce(knockback);

        //Debug.Log("Hit: hp = " + curHealth);
    }

    public void applyAttack(float damage)
    {
        curHealth -= damage;
    }
}
