using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public float bigMeteorSpeed;
    public float mediumMeteorSpeed;
    public float smallMeteorSpeed;
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = Random.Range(1f, 4f);

        player = GameObject.FindGameObjectWithTag("Player");
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

    public void CheckScale()
    {
        if (scale <= 1.5f) CheckMeteorSpeed(smallMeteorSpeed);
        else if (scale <= 2.8f) CheckMeteorSpeed(mediumMeteorSpeed);
        else if (scale <= 4f) CheckMeteorSpeed(bigMeteorSpeed);
    }
}
