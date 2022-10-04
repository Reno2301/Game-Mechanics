using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject meteor;
    public GameObject planet;

    private Vector2 spawnPos;
    public int minSpawnRange;
    public int maxSpawnRange;

    [Header("Meteors")]
    public int meteorCount;
    public float meteorSpawnTime;
    private float meteorTimer;

    [Header("Planets")]
    public int planetCount;
    public float planetSpawnTime;
    private float planetTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meteorTimer = meteorSpawnTime;
        planetTimer = planetSpawnTime;
    }

    private void Update()
    {
        meteorTimer += Time.deltaTime;
        planetTimer += Time.deltaTime;

        if (meteorTimer > meteorSpawnTime)
        {
            SpawnObjects(meteor, meteorCount);
            meteorTimer = 0;
        }

        if (planetTimer > planetSpawnTime)
        {
            SpawnObjects(planet, planetCount);
            planetTimer = 0;
        }
    }

    public void SetSpawnPosition()
    {
        spawnPos = new Vector2(
                Random.Range(player.transform.position.x - maxSpawnRange, player.transform.position.x + maxSpawnRange),
                Random.Range(player.transform.position.y - maxSpawnRange, player.transform.position.y + maxSpawnRange));
    }

    public void SpawnObjects(GameObject gameObject, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetSpawnPosition();

            float distance = Vector2.Distance(spawnPos, player.transform.position);

            while (distance < minSpawnRange)
            {
                SetSpawnPosition();
                distance = Vector2.Distance(spawnPos, player.transform.position);
            }

            Instantiate(gameObject, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        }
    }
}
