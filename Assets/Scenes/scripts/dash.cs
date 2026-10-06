using UnityEngine;
using UnityEngine.SceneManagement;

public class dash : MonoBehaviour
{
    public float speed = 5f;

    
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isDashing = false;

    private float dashTime;
    private float cooldownTime;

    private float direction = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");

        
        if (moveHorizontal != 0)
        {
            direction = Mathf.Sign(moveHorizontal);
        }

        
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(
                moveHorizontal * speed,
                rb.linearVelocity.y
            );
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDashing)
        {
            rb.AddForce(
                new Vector2(0f, 7f),
                ForceMode2D.Impulse
            );
        }

       
        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
        }

        
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            !isDashing &&
            cooldownTime <= 0)
        {
            StartDash();
        }

        
        if (isDashing)
        {
            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                EndDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;

        dashTime = dashDuration;
        cooldownTime = dashCooldown;

        
        rb.linearVelocity = new Vector2(
            direction * dashSpeed,
            0f
        );
    }

    void EndDash()
    {
        isDashing = false;

        
        rb.linearVelocity = new Vector2(
            0f,
            rb.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Dano"))
        {
            SceneManager.LoadScene("comite");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
   }
