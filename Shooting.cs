using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    private Vector3 mousePosition;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canShoot;
    private float timer;
    public float timeBetweenBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePosition - transform.position;

        float rotZ  = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if (canShoot == false)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenBullet )
            {
                canShoot = true;
                timer = 0f;
            }
        }

        if (Input.GetMouseButton(0) && canShoot == true)
        {
            canShoot = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
