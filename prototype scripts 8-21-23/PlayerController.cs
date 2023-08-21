using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int playerHealth;
    [SerializeField] public int playerMaxHealth;
    [SerializeField] float playerSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;

    private bool isFacingRight = true;
    private float defaultSpeed;
    public bool canDash = false;
    public bool isDashing = false;
    public bool playerCanMove = true;
    public bool playerAlive = true;

    Vector2 direction;
    Vector2 mousePosition;
    public Animator animator;

    private void Start()
    {
        defaultSpeed = playerSpeed;
        playerHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("SpeedX", Mathf.Abs(direction.x));
        animator.SetFloat("SpeedY", Mathf.Abs(direction.y));

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (playerHealth <= 0)
        {
            playerAlive = false;
            animator.SetBool("playerAlive", false);
            animator.SetBool("playerDead", true);

        }
        else
        {
            playerAlive = true;
        }

        if (Input.GetMouseButtonDown(1) && canDash == true && isDashing == false && playerCanMove == true && playerAlive == true)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (playerCanMove == true && playerAlive == true)
        {
            rb.MovePosition(rb.position + direction.normalized * playerSpeed * Time.deltaTime);
        }
    }

    void Flip()
    {
        if ( direction.x < -0.1f && isFacingRight == true && playerAlive == true)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
        else if ( direction.x > 0.1f  && isFacingRight == false && playerAlive == true)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 1;
            StartCoroutine(HitMaker());
        }

        if (collision.gameObject.CompareTag("SmallHealth") && playerHealth < playerMaxHealth)
        {
            Destroy(collision.gameObject);
            playerHealth += 1;
        }
        else if (collision.gameObject.CompareTag("ExtraHealth"))
        {
            Destroy(collision.gameObject);
            playerMaxHealth += 1;
        }
    }

    IEnumerator HitMaker()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        playerSpeed = dashSpeed;
        yield return new WaitForSeconds(0.1f);
        playerSpeed = defaultSpeed;
        isDashing = false;
        canDash = false;
        yield return new WaitForSeconds(2f);
        canDash = true;
    }
}