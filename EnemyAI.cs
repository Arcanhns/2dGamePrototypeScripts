using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform pathfinderPosition;
    [SerializeField] Transform playerPosition;
    [SerializeField] float health; // 5
    [SerializeField] float speed; // 4
    [SerializeField] float agroSpeed; // 5
    [SerializeField] float lowHealthSpeed; // 2
    [SerializeField] float slowedDown; // 2
    [SerializeField] float scoutingRange; // 7
    [SerializeField] float activeScoutingRange;
    [SerializeField] float agroRange; // 3

    private SpriteRenderer spriteRenderer;
    private float originalSpeed;

    [SerializeField] bool enraged = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        pathfinderPosition = GameObject.FindGameObjectWithTag("Pathfinder").transform;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

        float pathFinderOffSet = Vector2.Distance(pathfinderPosition.position, transform.position);
        float playerOffSet = Vector2.Distance(playerPosition.position, transform.position);

        if (pathFinderOffSet < scoutingRange && enraged == false)
        {
            scoutingRange = activeScoutingRange;
            transform.position = Vector2.MoveTowards(this.transform.position, pathfinderPosition.transform.position, speed * Time.deltaTime);
        }
        if (playerOffSet < agroRange || enraged == true)
        {
            enraged = true;
            transform.position = Vector2.MoveTowards(this.transform.position, playerPosition.transform.position, speed * Time.deltaTime);

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

    IEnumerator TakeDamage()
    {
        spriteRenderer.color = Color.red;
        speed = slowedDown;
        yield return new WaitForSeconds(0.1f);
        speed = originalSpeed;
        spriteRenderer.color = Color.white;
    }
}
