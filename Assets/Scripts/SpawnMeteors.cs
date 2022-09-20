using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteors : MonoBehaviour
{
    public GameObject player;
    public GameObject meteor;
    private Vector2 spawnPos;
    public int meteorCount;
    public int minSpawnRange;
    public int maxSpawnRange;
    public float spawnTime;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = spawnTime;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            SpawnRandomMeteors();
            timer = 0;
        }        
    }

    public void SetSpawnPosition()
    {
        spawnPos = new Vector2(
                Random.Range(player.transform.position.x - maxSpawnRange, player.transform.position.x + maxSpawnRange),
                Random.Range(player.transform.position.y - maxSpawnRange, player.transform.position.y + maxSpawnRange));
    }

    public void SpawnRandomMeteors()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            SetSpawnPosition();

            float distance = Vector2.Distance(spawnPos, player.transform.position);

            while (distance < minSpawnRange)
            {
                SetSpawnPosition();
                distance = Vector2.Distance(spawnPos, player.transform.position);
            }

            Instantiate(meteor, new Vector3(spawnPos.x, spawnPos.y, 0), Quaternion.identity);
        }
    }
}
