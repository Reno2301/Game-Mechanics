using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFast : MonoBehaviour
{
    private GameObject player;
    private Shooting shooting;
    private CircleCollider2D cc;
    private SpriteRenderer sr;
    public float duration;
    public float timer;
    public bool fireFaster;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shooting = player.GetComponentInChildren<Shooting>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        timer = 0;
    }

    private void Update()
    {
        if (fireFaster)
        {
            timer += Time.deltaTime;

            shooting.timeBetweenFiring = shooting.originTimeBetweenFiring * 0.5f;

            if (timer > duration)
            {
                shooting.timeBetweenFiring = shooting.originTimeBetweenFiring;
                fireFaster = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fireFaster = true;

            cc.enabled = false;
            sr.enabled = false;
        }
    }
}