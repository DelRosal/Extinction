using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_new : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public float walkMaxVel = 3f;
    public float walkAccel = 3f;
    public float sprintMaxVel = 5f;
    public float sprintAccel = 3f;
    public float jumpForce = 15f;
    public float jumpMultiplierVel = 4.5f;
    public float runJumpMultiplier = 1.2f;

    public float groundDrag = 1f;

    private float limitVelToDriftOnGroundTouch = 2.8f;
    private float jumpTime = 0f;
    private float maxJumpTime = 1.5f;
    private float varJumpMultiplier = 1f;

    private float jumpCount = 0;

    private Vector2 movAccel;
    private Vector2 movVel;

    private Rigidbody2D rb;
    private SpriteRenderer spr;

    private Vector2 lastInputVector;

    private bool isGrounded = false;
    private bool isSprinting = false;
    private bool wasSprinting = false;
    private bool facedAWall = false;
    private bool foundWallOnLeft = false;
    private bool foundWallOnRight = false;
    private bool hasDoubleJumped = false;
    private bool wasOnSky = false;
    private bool isJumping = false;

    //Audio Source jumping
    [SerializeField] private AudioSource jumpingSound;
    void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce * varJumpMultiplier, ForceMode2D.Impulse);
        jumpingSound.Play();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        movAccel.x = walkAccel;

        lastInputVector = new Vector2(0f, 0f);
    }

    void Update()
    {
        float mass = rb.mass;
        float playerXDir = (movVel.x / Mathf.Abs(movVel.x));

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0) { anim.SetBool("isWalking", true); }
        else { anim.SetBool("isWalking", false); }

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector.Normalize();

        if (movVel.x * (movVel.x / Mathf.Abs(movVel.x)) >= jumpMultiplierVel / 40) varJumpMultiplier = runJumpMultiplier;
        else varJumpMultiplier = 1f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //We start the jumping animation
            anim.SetBool("IsJumping", true);
            
            if (inputVector.x != 0)
            {
                if (movVel.x / Mathf.Abs(movVel.x) != inputVector.x) movVel.x = 0;
            }

            if (!hasDoubleJumped)
            {
                isJumping = true;
                jumpTime = 0f;
                Jump();
                jumpCount++;
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            // Else we stop the jumping animation
            anim.SetBool("IsJumping", false);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTime < maxJumpTime)
            {
                jumpTime += Time.deltaTime;
                rb.AddForce(Vector2.up * jumpForce * varJumpMultiplier * Time.deltaTime, ForceMode2D.Impulse);
            }
        }

        if (jumpCount > 0) hasDoubleJumped = true;

        if (isGrounded)
        {
            hasDoubleJumped = false;
            jumpCount = 0;
            jumpTime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        // Movimiento

        if (isSprinting) movAccel.x = sprintAccel;
        else movAccel.x = walkAccel;

        if (inputVector.x > 0) movVel.x += movAccel.x * Time.deltaTime * (0.1f);
        else if (inputVector.x < 0) movVel.x -= movAccel.x * Time.deltaTime * (0.1f);
        else
        {
            if (movVel.x > 0f) movVel.x -= groundDrag * Time.deltaTime * (0.1f);
            else if (movVel.x < 0f) movVel.x += groundDrag * Time.deltaTime * (0.1f);
        }

        if (movVel.x < 0.0001 && movVel.x > -0.0001) movVel.x = 0;

        float maxVel = walkMaxVel;
        if (isSprinting)
        {
            maxVel = sprintMaxVel;
            wasSprinting = true;
        }

        maxVel /= 40;

        if (inputVector.x != 0)
        {
            if (!isSprinting && wasSprinting)
            {
                movVel.x *= 0.98f;
                if (Mathf.Abs(movVel.x) <= walkMaxVel / 40) wasSprinting = false;
            }
            else movVel.x = Mathf.Clamp(movVel.x, -maxVel, maxVel);
        }

        if (facedAWall)
        {
            if (foundWallOnLeft && inputVector.x == -1) movVel.x = 0;
            if (foundWallOnRight && inputVector.x == 1) movVel.x = 0;
        }

        float maxVelFromSky = limitVelToDriftOnGroundTouch / 40;
        if (Mathf.Abs(movVel.x) < maxVelFromSky && wasOnSky && isGrounded) movVel.x = 0;

        // Renderizado

        if (isGrounded || (!isGrounded && (movVel.x * (movVel.x / Mathf.Abs(movVel.x))) * 40 < 2))
        {
            if (inputVector.x > 0) spr.flipX = false;
            else if (inputVector.x < 0) spr.flipX = true;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(movVel.x, 0, 0, Space.World);
    }

    void LateUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        lastInputVector = new Vector2(horizontalInput, verticalInput);
        if (!isGrounded) wasOnSky = true;
        else wasOnSky = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Verificar si el objeto con el que colisionó es el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Obtener todas las contact points de la colisión
            ContactPoint2D[] contacts = collision.contacts;

            // Iterar sobre todas las contact points
            bool foundGrounded = false;
            foundWallOnLeft = false;
            foundWallOnRight = false;
            foreach (ContactPoint2D contact in contacts)
            {
                Vector2 playerPoint = transform.position;
                Vector2 relativePosition = playerPoint - contact.point;
                if ((contact.normal.x > 0 && relativePosition.x > 0)) foundWallOnLeft = true;
                if ((contact.normal.x < 0 && relativePosition.x < 0)) foundWallOnRight = true;

                // Verificar si la normal está apuntando hacia arriba
                if (contact.normal.y > 0.9f) foundGrounded = true;
            }

            if (foundWallOnLeft || foundWallOnRight) facedAWall = true;
            else facedAWall = false;

            if (foundGrounded) isGrounded = true;
            else isGrounded = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            facedAWall = false;
            foundWallOnLeft = false;
            foundWallOnRight = false;
        }
    }

}