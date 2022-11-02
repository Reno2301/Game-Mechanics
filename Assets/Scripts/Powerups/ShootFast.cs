using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFast : MonoBehaviour
{
    public GameObject player;
    public Shooting shooting;
    public float timer;
    public float duration;
    public CircleCollider2D cc;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shooting = player.GetComponentInChildren<Shooting>();

        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        shooting.timeBetweenFiring = shooting.originTimeBetweenFiring;
        shooting.timerPowerUp = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cc.enabled = false;
            sr.enabled = false;

            timer += Time.deltaTime;

            shooting.timeBetweenFiring = shooting.originTimeBetweenFiring * 0.5f;

            if (shooting.timerPowerUp > duration)
            {
                shooting.timeBetweenFiring = shooting.originTimeBetweenFiring;
                Destroy(gameObject);
            }
        }
    }
}