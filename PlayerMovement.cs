using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float dash = 5f;
    public bool isDashing = false;
    public bool isDashReady = true;
    [SerializeField] Rigidbody2D rb;

    Vector2 direction;

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse1) && isDashReady == true)
        {
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing == false)
        {
            rb.MovePosition(rb.position + direction.normalized * playerSpeed * Time.fixedDeltaTime);
        }
        else if (isDashing == true)
        {
            StartCoroutine(Dash());
        }
        
    }

    IEnumerator Dash()
    {
        rb.MovePosition(rb.position + direction.normalized * dash * Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.1f);
        isDashing = false;
        isDashReady = false;
        yield return new WaitForSeconds(3f);
        isDashReady = true;
    }
}
