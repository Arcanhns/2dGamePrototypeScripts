using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBossAi : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] GameObject batMinion;
    [SerializeField] GameObject dropOnDeath;
    [SerializeField] Transform batSpawner;
    [SerializeField] BossHealthBar healthBar;
    [SerializeField] float health; // 5
    [SerializeField] float maxHealth;
    [SerializeField] float speed; // 4
    [SerializeField] float enragedSpeed;
    [SerializeField] float scoutingRange; // 7
    [SerializeField] float activeScoutingRange;
    [SerializeField] float shootingRange; // 3

    private SpriteRenderer spriteRenderer;
    public bool canSpawnBat = true;
    public bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponent<BossHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        float playerOffSet = Vector2.Distance(playerPosition.position, transform.position);
        Flip();

        if (playerOffSet < scoutingRange && playerOffSet > shootingRange)
        {
            scoutingRange = activeScoutingRange;
            
            transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.transform.position, speed * Time.deltaTime);

            if (health > 7 && canSpawnBat == true)
            {
                StartCoroutine(SpawnMinions(3f));
            }

            if (health < 7 && canSpawnBat == true)
            {
                speed = enragedSpeed;
                StartCoroutine(SpawnMinions(2f));
            }
        }
        if (playerOffSet < shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.transform.position, -speed * Time.deltaTime);
        }

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scoutingRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(TakeDamage());

            if (health == 0)
            {
                Instantiate(dropOnDeath, transform.localPosition, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void Flip()
    {
        if (transform.position.x > playerPosition.position.x)
        {
            isFacingRight = false;
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x < playerPosition.position.x)
        {
            isFacingRight = true;
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator TakeDamage()
    {
        if ( scoutingRange < activeScoutingRange)
        {
            scoutingRange = activeScoutingRange;
        }

        health -= 1;
        healthBar.UpdateHealthBar(health, maxHealth);

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator SpawnMinions(float spawnTimer)
    {
        Instantiate(batMinion, batSpawner.position, Quaternion.identity);
        canSpawnBat = false;
        yield return new WaitForSeconds(spawnTimer);
        canSpawnBat = true;
    }

}

