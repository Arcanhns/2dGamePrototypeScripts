using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] public float health = 3;
    public float maxHealth;
    private SpriteRenderer enemySprite;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;
    }
}
