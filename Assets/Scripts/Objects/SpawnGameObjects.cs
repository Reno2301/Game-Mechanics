using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject meteor;
    public GameObject planet;
    public GameObject enemy;
    public GameObject blackHole;

    private Vector2 spawnPos;

    [Header("Meteors")]
    public int meteorCount;
    public int minSpawnRangeMeteor;
    public int maxSpawnRangeMeteor;
    public float meteorSpawnTime;
    private float meteorTimer;

    [Header("Planets")]
    public int planetCount;
    public int minSpawnRangePlanet;
    public int maxSpawnRangePlanet;
    public float planetSpawnTime;
    private float planetTimer;

    [Header("Enemies")]
    public int enemyCount;
    public int minSpawnRangeEnemy;
    public int maxSpawnRangeEnemy;
    public float enemyMinSpawnTime;
    public float enemyMaxSpawnTime;
    public float enemySpawnTime;
    private float enemyTimer;    
    
    [Header("Black Hole")]
    public int bhCount;
    public int minSpawnRangeBh;
    public int maxSpawnRangeBh;
    public float bHSpawnTime;
    private float bHTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meteorTimer = meteorSpawnTime;
        planetTimer = planetSpawnTime;
        enemyTimer = 0;
        bHTimer = bHSpawnTime;

        enemySpawnTime = Random.Range(enemyMinSpawnTime, enemyMaxSpawnTime);
    }

    private void Update()
    {
        meteorTimer += Time.deltaTime;
        planetTimer += Time.deltaTime;
        enemyTimer += Time.deltaTime;
        bHTimer += Time.deltaTime;

        if (meteorTimer > meteorSpawnTime)
        {
            SpawnObjects(meteor, minSpawnRangeMeteor, maxSpawnRangeMeteor, meteorCount);
            meteorTimer = 0;
        }

        if (planetTimer > planetSpawnTime)
        {
            SpawnObjects(planet, minSpawnRangePlanet, maxSpawnRangePlanet, planetCount);
            planetTimer = 0;
        }

        if (enemyTimer > enemySpawnTime)
        {
            SpawnObjects(enemy, minSpawnRangeEnemy, maxSpawnRangeEnemy, enemyCount);
            enemySpawnTime = Random.Range(enemyMinSpawnTime, enemyMaxSpawnTime);
            enemyTimer = 0;
        }
        
        if (bHTimer > bHSpawnTime)
        {
            SpawnObjects(blackHole, minSpawnRangeBh, maxSpawnRangeBh, bhCount);
            bHTimer = 0;
        }
    }

    public void SetSpawnPosition(int maxSpawnRange)
    {
        spawnPos = new Vector2(
                Random.Range(player.transform.position.x - maxSpawnRange, player.transform.position.x + maxSpawnRange),
                Random.Range(player.transform.position.y - maxSpawnRange, player.transform.position.y + maxSpawnRange));
    }

    public void SpawnObjects(GameObject gameObject, int minSpawnRange, int maxSpawnRange, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetSpawnPosition(maxSpawnRange);

            float distance = Vector2.Distance(spawnPos, player.transform.position);

            while (distance < minSpawnRange)
            {
                SetSpawnPosition(maxSpawnRange);
                distance = Vector2.Distance(spawnPos, player.transform.position);
            }

            Instantiate(gameObject, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        }
    }
}
