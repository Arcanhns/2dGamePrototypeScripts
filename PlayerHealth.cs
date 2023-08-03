using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHp = 100;
    [SerializeField] HealthPackSpawner healthPackSpawner;

    // Start is called before the first frame update
    void Start()
    {
        playerHp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            playerHp -= 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HealthPack" && playerHp <= 90)
        {
            playerHp += 10;
            Destroy(collision.gameObject);
            healthPackSpawner.pickedUp = true;
        }
    }
}
