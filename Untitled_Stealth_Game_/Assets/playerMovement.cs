using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (moveSpeed <= 0) { moveSpeed = 1; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector2.right * moveSpeed;
            if (rb.velocity.x > -moveSpeed)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            rb.velocity.Normalize();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector2.left* moveSpeed;
            if (rb.velocity.x < -moveSpeed)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
            rb.velocity.Normalize();

        }
        if (Input.GetKey(KeyCode.S))
        {
           rb.velocity += Vector2.down * moveSpeed;
            if (rb.velocity.y < -moveSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
            }
            rb.velocity.Normalize();

        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * moveSpeed; //transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            if(rb.velocity.y > moveSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            }
            rb.velocity.Normalize();

        }
        if (!Input.anyKey)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BOOM"))
        {
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Missle"))
        {
            Destroy(this.gameObject);
        }
    }
}
