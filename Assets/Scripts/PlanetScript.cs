using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject bullet;
    public SpriteRenderer sr;
    public GameObject crystal;

    [Header("Planets")]
    public int planetNr;
    public Sprite[] planets;

    public int lives = 5;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        planetNr = (int)Random.Range(0, 14);
        SetSprite();
    }


    public void SetSprite()
    {
        string spriteName = sr.sprite.name;
        spriteName = spriteName.Replace("planet", "");
        int spriteNr = int.Parse(spriteName);

        sr.sprite = planets[planetNr];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        if (collision.gameObject.tag == "Bullet")
        {
            lives--;
        }

        if (lives <= 0)
        {
            PlanetDies();
        }
    }

    void PlanetDies()
    {
        Instantiate(crystal, this.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
