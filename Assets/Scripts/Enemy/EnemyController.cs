using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public int lives;
    [SerializeField] private SimpleFlash flashEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
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
}
