using UnityEngine;
using static GameManager;

[DefaultExecutionOrder(-100)]
public class EnemyStats : MonoBehaviour, Stats
{
    // Stores Player Stats (Health, Damage, etc) and provides tools for modifying those values

    [SerializeField] Host enemyType = Host.Worm;

    float curHealth;
    float curSpeed;
    float curDamage;
    Host curType;
    //Condition curCondition = Condition.Alive;

    const float hpDrain = 1f; // amount of hp lost every second

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (enemyType == Host.Worm)
        {
            // no specific enemy chosen. Wait for outside script to set type
        }
        else
        {
            setType(enemyType);
        }
    }

    public struct NPCStats
    {
        public float health;
        public float speed;
        public float damage;
        public Host type;

        public NPCStats(float curHealth, float curSpeed, float curDamage, Host curType)
        {
            health = curHealth;
            speed = curSpeed;
            damage = curDamage;
            type = curType;
        }
    }

    public void setType(Host newType)
    {
        //Debug.Log("set to: " + newType);
        curType = newType;
        enemyType = newType; // mainly for visibility in editor

        switch (newType)
        {
            case Host.Cat:
                curHealth = 5;
                curSpeed = 3.5f;
                curDamage = 1;
                break;

            case Host.Dog:
                curHealth = 5;
                curSpeed = 3;
                curDamage = 3;
                break;

            case Host.Child:
                curHealth = 15;
                curSpeed = 2.5f;
                curDamage = 2;
                break;

            case Host.Adult:
                curHealth = 20;
                curSpeed = 2;
                curDamage = 5;
                break;

            case Host.Cop:
                curHealth = 20;
                curSpeed = 1;
                curDamage = 10;
                break;
        }
    }


    public NPCStats getStats()
    {
        //Debug.Log("dmg: " + curDamage);
        return new NPCStats(curHealth, curSpeed, curDamage, curType);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player Attack"))
        {
            PlayerAttack attack = col.gameObject.GetComponentInParent<PlayerAttack>();

            if (attack == null)
            {
                Debug.LogError("Couldn't find player attack");
            }
            else
            {
                
                curHealth -= attack.getDamage();
                //Debug.Log("enemy took damage. HP: " + curHealth);

                Vector2 playerPos = PlayerObject.getPlayer().transform.position;
                Vector2 dir = -(playerPos - (Vector2)transform.position).normalized;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(dir * attack.getKnockback());
            }

        }
    }

    public void applyAttack(float damage)
    {
        curHealth -= damage;
    }

    private void Update()
    {
        if (curHealth <= 0)
        {
            GetComponent<EnemyDeath>().onDeath();
        }
    }
}
