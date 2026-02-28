using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Stores Player Stats (Health, Damage, etc) and provides tools for modifying those values

    [SerializeField] Type enemyType;

    float curHealth;
    float curSpeed;
    float curDamage;
    Type curType;
    Condition curCondition = Condition.Alive;

    const float hpDrain = 1f; // amount of hp lost every second

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setType(enemyType);
    }
    public enum Type
    {
        Cat,
        Dog,
        Child,
        Adult,
        Cop
    };

    public enum Condition
    {
        Alive,
        Dead
    };

    public struct NPCStats
    {
        public float health;
        public float speed;
        public float damage;
        public Type type;

        public NPCStats(float curHealth, float curSpeed, float curDamage, Type curType)
        {
            health = curHealth;
            speed = curSpeed;
            damage = curDamage;
            type = curType;
        }
    }

    public void setType(Type newType)
    {
        curType = newType;

        switch (newType)
        {
            case Type.Cat:
                curHealth = 10;
                curSpeed = 5;
                curDamage = 1;
                break;

            case Type.Dog:
                curHealth = 15;
                curSpeed = 4;
                curDamage = 3;
                break;

            case Type.Child:
                curHealth = 20;
                curSpeed = 3;
                curDamage = 5;
                break;

            case Type.Adult:
                curHealth = 30;
                curSpeed = 2;
                curDamage = 10;
                break;

            case Type.Cop:
                curHealth = 30;
                curSpeed = 2;
                curDamage = 20;
                break;
        }
    }


    public NPCStats getStats()
    {
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

                Vector2 playerPos = PlayerObject.getPlayer().transform.position;
                Vector2 dir = -(playerPos - (Vector2)transform.position).normalized;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(dir * attack.getKnockback());
            }

        }
    }
}
