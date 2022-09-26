using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public GameObject bullet;

    public float smallMeteorSpeed;
    public float mediumMeteorSpeed;
    public float bigMeteorSpeed;

    public int smallMeteorLives;
    public int mediumMeteorLives;
    public int bigMeteorLives;

    private int lives;

    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = GameObject.FindGameObjectWithTag("Bullet");

        scale = (int)Random.Range(1, 4);
        transform.localScale = new Vector3(scale, scale, scale);
        CheckScale();
    }

    public void CheckMeteorSpeed(float meteorSpeed)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 meteorPos = this.transform.position;

        if (meteorPos.x > playerPos.x) rb.velocity = new Vector2(-meteorSpeed, rb.velocity.y);
        else rb.velocity = new Vector2(meteorSpeed, rb.velocity.y);

        if (meteorPos.y > playerPos.y) rb.velocity = new Vector2(rb.velocity.x, -meteorSpeed);
        else rb.velocity = new Vector2(rb.velocity.x, meteorSpeed);
    }

    public void CheckMeteorLives(int lives)
    {
        this.lives = lives;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Bullet")
        {
            lives--;
        }

        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CheckScale()
    {
        if (scale <= 1f)
        {
            CheckMeteorSpeed(smallMeteorSpeed);
            CheckMeteorLives(smallMeteorLives);
        }
        else if (scale <= 2f)
        {
            CheckMeteorSpeed(mediumMeteorSpeed);
            CheckMeteorLives(mediumMeteorLives);
        }

        else if (scale <= 3f)
        {
            CheckMeteorSpeed(bigMeteorSpeed);
            CheckMeteorLives(bigMeteorLives);
        }
    }
}
