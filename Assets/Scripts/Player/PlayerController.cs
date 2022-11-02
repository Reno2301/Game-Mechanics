using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SimpleFlash flashEffect;
    public ParticleSystem bloodParticle;
    public ParticleSystem pickUpParticle;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject panel;
    private ScoreHandler sh;
    private HighScore hs;

    [Header("Movement")]
    public float startSpeed;
    public float moveSpeed;
    private Vector2 moveDirection;
    public float movementDrag;

    [Header("Health")]
    public int lives = 3;

    public int bulletDamage;
    public int meteorDamage;

    public float timeBetweenHits;
    private float hitTimer;
    private bool canGetHit;

    private void Start()
    {
        sh = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreHandler>();
        hs = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<HighScore>();
        moveSpeed = startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHit();
        CheckDead();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canGetHit)
        {
            if (collision.gameObject.tag == "Meteor")
            {
                GetHitByMeteor();
            }

            if (collision.gameObject.tag == "EnemyBullet")
            {
                GetHitByEnemyBullet();
            }
        }
    }

    private void GetHitByMeteor()
    {
        flashEffect.Flash();
        bloodParticle.Play();
        lives -= meteorDamage;
        canGetHit = false;
    }

    private void GetHitByEnemyBullet()
    {
        flashEffect.Flash();
        bloodParticle.Play();
        lives -= bulletDamage;
        canGetHit = false;
    }

    public void GetHitByBlackHole()
    {
        flashEffect.Flash();
        lives = 0;
        canGetHit = false;
    }

    private void CheckDead()
    {
        if (lives == 0)
        {
            panel.gameObject.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            sh.CheckPoints();
            hs.SetHighScore();

            lives = -1;
        }
    }

    private void CheckHit()
    {
        if (!canGetHit)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer > timeBetweenHits)
            {
                canGetHit = true;
                hitTimer = 0;
            }
        }
    }

    private void Movement()
    {
        // Second movement (steering through space)
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-moveSpeed, 0), ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(moveSpeed, 0), ForceMode2D.Force);
        }


        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, moveSpeed), ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector2(0, -moveSpeed), ForceMode2D.Force);
        }

        rb.velocity *= movementDrag;
    }
}
