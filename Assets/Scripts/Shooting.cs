using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=-bkmPm_Besk

public class Shooting : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    private Camera mainCam;
    public GameObject bullet;
    public Transform bulletTransform;
    private Vector3 mousePos;
    private bool canFire;
    private float timer;
    public float timeBetweenFiring;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
/*
        fieldOfView.SetAimDirection(rotation);

        fieldOfView.SetOrigin(transform.position);*/
    }
}
