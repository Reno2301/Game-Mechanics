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
    private ScoreHandler sh;
    private HighScore hs;

    [Header("Movement")]
    public float moveSpeed;
    private Vector2 moveDirection;
    public float movementDrag;

    [Header("Health")]
    public int lives = 3;

    public float timeBetweenHits;
    private float hitTimer;
    private bool canGetHit;

    private void Start()
    {
        sh = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreHandler>();
        hs = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<HighScore>();
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
        if (collision.gameObject.tag == "Meteor" && canGetHit)
        {
            lives--;
            canGetHit = false;
        }
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
