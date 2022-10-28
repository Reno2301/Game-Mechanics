using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private Rigidbody2D playerRb;

    public float pullDistance;
    public float deathDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.rotation += 8;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < pullDistance)
        {
            Vector2 desiredVelocity = transform.position - player.transform.position;
            desiredVelocity.Normalize();
            desiredVelocity *= 0.5f;

            Vector2 steeringForce = desiredVelocity - playerRb.velocity;
            steeringForce.Normalize();
            steeringForce *= 0.01f;

            playerRb.velocity += steeringForce;
        }

        if(distance < deathDistance)
        {
            player.GetComponent<PlayerController>().GetHitByBlackHole();
        }
    }
}
