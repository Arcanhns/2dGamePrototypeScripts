using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform pathfinderPosition;
    [SerializeField] Transform playerPosition;
    [SerializeField] SpriteRenderer enemySprite;
    [SerializeField] SpriteRenderer spottedSprite;
    [SerializeField] float health; // 5
    [SerializeField] float speed; // 4
    [SerializeField] float agroSpeed; // 5
    [SerializeField] float lowHealthSpeed; // 2
    [SerializeField] float slowedDown; // 2
    [SerializeField] float scoutingRange; // 7
    [SerializeField] float activeScoutingRange;
    [SerializeField] float agroRange; // 3
    [SerializeField] bool isFacingRight = true;

    private SpriteRenderer spriteRenderer;
    private float originalSpeed;
    private bool spotted = false;
    private Vector2 originalPosition;
    public Animator animator;

    [SerializeField] bool enraged = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spottedSprite.enabled = false;
        originalPosition = new Vector2 (transform.position.x, transform.position.y);
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        pathfinderPosition = GameObject.FindGameObjectWithTag("Pathfinder").transform;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        float pathFinderOffSet = Vector2.Distance(pathfinderPosition.position, transform.position);
        float playerOffSet = Vector2.Distance(playerPosition.position, transform.position);

        if (playerController.playerAlive == false)
        {
            animator.SetBool("Chasing", false);
            transform.position = Vector2.MoveTowards(this.transform.position, originalPosition, speed * Time.deltaTime);
        }

        if (pathFinderOffSet < scoutingRange && enraged == false && playerController.playerAlive == true)
        {
            Flip();
            animator.SetBool("Chasing", true);
            scoutingRange = activeScoutingRange;
            transform.position = Vector2.MoveTowards(this.transform.position, pathfinderPosition.transform.position, speed * Time.deltaTime);
        }
        if (playerOffSet < agroRange && playerController.playerAlive == true || enraged == true && playerController.playerAlive == true)
        {
            Flip();
            animator.SetBool("Chasing", true);
            enraged = true;

            transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.transform.position, speed * Time.deltaTime);

            if (spotted == false)
            {
                StartCoroutine(Spotted());
            }

            if (health > 1)
            {
                speed = agroSpeed;
            }
            else
            {
                speed = lowHealthSpeed;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scoutingRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(TakeDamage());
            enraged = true;
            health -= 1;

            if (health == 0)
            {
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

    IEnumerator Spotted()
    {
        spotted = true;
        spottedSprite.enabled = true;
        yield return new WaitForSeconds(1);
        spottedSprite.enabled = false;
    }

    IEnumerator TakeDamage()
    {
        spriteRenderer.color = Color.red;
        speed = slowedDown;
        yield return new WaitForSeconds(0.1f);
        speed = originalSpeed;
        spriteRenderer.color = Color.white;
    }
}
