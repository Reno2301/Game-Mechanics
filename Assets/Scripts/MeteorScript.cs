using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public GameObject bullet;
    [SerializeField] private SimpleFlash flashEffect;

    [Header("Scale")]
    public int smallMeteorScale = 1;
    public int mediumMeteorScale = 2;
    public int bigMeteorScale = 3;

    [Header("Speed")]
    public float smallMeteorSpeed;
    public float mediumMeteorSpeed;
    public float bigMeteorSpeed;

    [Header("Lives")]
    public int smallMeteorLives;
    public int mediumMeteorLives;
    public int bigMeteorLives;

    [Header("TrailWidth")]
    public float trailWidthFactor = 0.9f;

    private TrailRenderer tr;

    private int lives;

    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        tr = GetComponent<TrailRenderer>();

        scale = (int)Random.Range(smallMeteorScale, bigMeteorScale + 1);
        transform.localScale = new Vector3(scale, scale, scale);
        CheckScale();
        CheckMeteorTrailWidth();
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

    public void CheckMeteorTrailWidth()
    {
        tr.startWidth = scale * trailWidthFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            lives--;
            flashEffect.Flash();
            Destroy(collision.gameObject);
        }

        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CheckScale()
    {
        if (scale <= smallMeteorScale)
        {
            CheckMeteorSpeed(smallMeteorSpeed);
            CheckMeteorLives(smallMeteorLives);
            tr.time = 1;
        }
        else if (scale <= mediumMeteorScale)
        {
            CheckMeteorSpeed(mediumMeteorSpeed);
            CheckMeteorLives(mediumMeteorLives);
            tr.time = 3;
        }

        else if (scale <= bigMeteorScale)
        {
            CheckMeteorSpeed(bigMeteorSpeed);
            CheckMeteorLives(bigMeteorLives);
            tr.time = 5;
        }
    }
}
