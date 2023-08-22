using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    [SerializeField] float detectRange;
    [SerializeField] SpriteRenderer spottedSprite;
    public LayerMask obstacleLayer;

    PlayerController playerController;
    private Transform player;
    SpriteRenderer enemySprite;
    private bool hasSpottedPlayer = false;
    private bool spotted = false;
    private Vector3 lastSeenPosition;
    private bool isFacingRight = true;
    private Vector2 originalPos;
    public EnemyStats enemyHealth;

    NavMeshAgent agent;

    private void Start()
    {
        originalPos = transform.position;
        enemyHealth = GetComponent<EnemyStats>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        GameObject playerObject = GameObject.FindWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        enemySprite = GetComponent<SpriteRenderer>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spottedSprite.enabled = false;
    }

    private void Update()
    {

        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectRange && CanSeePlayer() && playerController.playerAlive)
        {
            if (!spotted)
            {
                StartCoroutine(Spotted());
            }

            Flip();
            hasSpottedPlayer = true;
            lastSeenPosition = player.position;
            ChasePlayer();
        }
        else if (hasSpottedPlayer)
        {
            MoveToLastSeenPosition();
        }

        if (playerController.playerAlive == false)
        {
            agent.SetDestination(originalPos);
        }

        if (enemyHealth.health != enemyHealth.maxHealth && playerController.playerAlive)
        {
            Flip();
            agent.SetDestination(player.position);
        }

    }

    void Flip()
    {
        if (transform.position.x > player.position.x)
        {
            isFacingRight = false;
            enemySprite.flipX = true;
        }
        else if (transform.position.x < player.position.x)
        {
            isFacingRight = true;
            enemySprite.flipX = false;
        }
    }

    IEnumerator Spotted()
    {
        spotted = true;
        spottedSprite.enabled = true;
        yield return new WaitForSeconds(1);
        spottedSprite.enabled = false;
    }

        private bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, obstacleLayer);

        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            return false;
        }

        return true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void MoveToLastSeenPosition()
    {
        agent.SetDestination(lastSeenPosition);
        spotted = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = player != null ? Vector3.Distance(transform.position, player.position) : Mathf.Infinity;

        // Draw the detection range circle
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        // Draw the line between enemy and player only if the player is within the detection range
        if (player != null && distanceToPlayer <= detectRange)
        {
            if (hasSpottedPlayer)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawLine(transform.position, player.position);
        }

        // Visualize the last seen position as a blue sphere
        if (hasSpottedPlayer)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(lastSeenPosition, 0.2f);
        }

    }

}
