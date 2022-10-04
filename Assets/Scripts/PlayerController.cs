using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public GameObject panel;

    [Header("Movement")]
    public float moveSpeed;
    private Vector2 moveDirection;

    [Header("Health")]
    public int lives = 3;

    public float timeBetweenHits;
    private float hitTimer;
    private bool canGetHit;

    [Header("Score")]
    public Text scoreText;
    public int score;

    // Update is called once per frame
    void Update()
    {
        Inputs();
        CheckHit();
        CheckDead();
        scoreText.text = score.ToString();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meteor" && canGetHit)
        {
            lives--;
            canGetHit = false;
        }
    }

    private void CheckDead()
    {
        if(lives <= 0)
        {
            panel.gameObject.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
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

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
