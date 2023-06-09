using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed = 200f;
    private bool isFacingRight = true;

    public Rigidbody2D rb;
    Vector2 movement;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");
       rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
       Flip();

        if (movement.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    //We create a function to flip our player depending on the direction he is moving
    private void Flip()
    {
        if (movement.x < 0 ){
            spriteRenderer.flipX = true;
        } else spriteRenderer.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
        }
    }

   // We create a function to lock the player movement until the animation of hatching is done
    public void LockMovement()
    {
        speed = 0f;
    }

    // We create a function to unlock the player movement after the animation of hatching is done
    public void UnlockMovement()
    {
        speed = 200f;
    }
}

