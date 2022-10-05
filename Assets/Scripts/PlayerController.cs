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
    public GameObject crystal;
    private CrystalScript cs;

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

    public Text crystalAmount0;
    public Text crystalAmount1;
    public Text crystalAmount2;
    public Text crystalAmount3;
    public Text crystalAmount4;
    public Text crystalAmount5;

    public Text crystalScore0;
    public Text crystalScore1;
    public Text crystalScore2;
    public Text crystalScore3;
    public Text crystalScore4;
    public Text crystalScore5;

    public Text totalPoints;

    public int countCrystal0;
    public int countCrystal1;
    public int countCrystal2;
    public int countCrystal3;
    public int countCrystal4;
    public int countCrystal5;

    private void Start()
    {
        cs = crystal.GetComponent<CrystalScript>();
    }

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
        if (lives <= 0)
        {
            panel.gameObject.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            int crystal0Points = cs.crystalPoint0 * countCrystal0;
            int crystal1Points = cs.crystalPoint1 * countCrystal1;
            int crystal2Points = cs.crystalPoint2 * countCrystal2;
            int crystal3Points = cs.crystalPoint3 * countCrystal3;
            int crystal4Points = cs.crystalPoint4 * countCrystal4;
            int crystal5Points = cs.crystalPoint5 * countCrystal5;

            crystalScore0.text = countCrystal0.ToString() + "x - Points: ";
            crystalScore1.text = countCrystal1.ToString() + "x - Points: ";
            crystalScore2.text = countCrystal2.ToString() + "x - Points: ";
            crystalScore3.text = countCrystal3.ToString() + "x - Points: ";
            crystalScore4.text = countCrystal4.ToString() + "x - Points: ";
            crystalScore5.text = countCrystal5.ToString() + "x - Points: ";

            crystalAmount0.text = crystal0Points.ToString();
            crystalAmount1.text = crystal1Points.ToString();
            crystalAmount2.text = crystal2Points.ToString();
            crystalAmount3.text = crystal3Points.ToString();
            crystalAmount4.text = crystal4Points.ToString();
            crystalAmount5.text = crystal5Points.ToString();

            int totalPoints = crystal0Points + crystal1Points + crystal2Points + crystal3Points + crystal4Points + crystal5Points;
            this.totalPoints.text = totalPoints.ToString();
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
