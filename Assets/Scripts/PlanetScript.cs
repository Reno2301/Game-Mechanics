using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject bullet;
    public GameObject crystal;
    private SpriteRenderer sr;
    [SerializeField] private SimpleFlash flashEffect;

    [Header("Planets")]
    public int planetNr;
    public Sprite[] planets;

    public int lives = 5;

    private Vector3 randomOffset;
    private int crystalCount;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        planetNr = (int)Random.Range(0, 14);
        SetSprite();

        crystalCount = (int)Random.Range(1, 5);
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
        if (collision.gameObject.tag == "Bullet")
        {
            lives--;
            flashEffect.Flash();
            Destroy(collision.gameObject);
        }

        if (lives <= 0)
        {
            PlanetDies();
        }
    }

    void PlanetDies()
    {
        for (int i = 0; i < crystalCount; i++)
        {
            randomOffset = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
            Instantiate(crystal, this.transform.position + randomOffset, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
