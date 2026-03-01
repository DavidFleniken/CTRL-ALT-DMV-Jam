using UnityEngine;
using static GameManager;
using System.Collections;

public class EnemySpawning : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] float noSpawnRadius = 11f;
    [SerializeField] float spawnRadius = 22f;

    [SerializeField] float timeBetweenSpawns = 10f;
    [SerializeField] float spawnTimeHalfLife = 120f; // secs it takes for timeBetweenSpawns to half (not really but close enough)

    [SerializeField] float curDifficulty = 1;
    [SerializeField] float difficultyIncreaseRate = 0.1f; // linear rate of difficulty increase

    const float deathDistance = 100; // when reach this distance from center, summon a bunch of people to force death

    private void Start()
    {
        player = PlayerObject.getPlayer();
        StartCoroutine(spawnTimer());
    }

    void spawnEnemy(Host enemyType)
    {
        // generate a random vector with magnitude between noSpawnRadius and SpawnRadius
        Vector2 spawnVec = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
        spawnVec *= Random.Range(noSpawnRadius, spawnRadius);

        GameObject newEnemy = Instantiate(enemyPrefab, (Vector2)player.transform.position + spawnVec, enemyPrefab.transform.rotation);
        newEnemy.GetComponent<EnemyStats>().setType(enemyType);
    }

    private void Update()
    {
        timeBetweenSpawns -= 0.5f * timeBetweenSpawns * Time.deltaTime / spawnTimeHalfLife;
        curDifficulty += difficultyIncreaseRate * Time.deltaTime;

        if(player.transform.position.magnitude > deathDistance)
        {
            timeBetweenSpawns = 0.1f;
            curDifficulty = 10;
        }
        //Debug.Log("Time: " + timeBetweenSpawns);
    }

    IEnumerator spawnTimer()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        Host enemy = randomEnemy();
        spawnEnemy(enemy);
        StartCoroutine(spawnTimer());
    }

    Host randomEnemy()
    {
        float choice = Random.Range(0, curDifficulty);
        Host enemy;

        if (choice < 1)
        {
            enemy = Host.Cat;
        }
        else if (choice < 2)
        {
            enemy = Host.Dog;
        }
        else if (choice < 3)
        {
            enemy = Host.Child;
        }
        else if (choice < 4)
        {
            enemy = Host.Adult;
        }
        else
        {
            enemy = Host.Cop;
        }

        return enemy;
    }
}
