using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_new : MonoBehaviour {
    public float walkMaxVel = 3f;
    public float walkAccel = 3f;
    public float sprintMaxVel = 5f;
    public float sprintAccel = 3f;
    public float jumpForce = 14f;
    
    public float groundDrag = 1f;

    private float limitVelToDriftOnGroundTouch = 4f;

    private float jumpCount = 0;

    private Vector2 movAccel;
    private Vector2 movVel;

    private Rigidbody2D rb;

    private Vector2 lastInputVector;

    private bool isGrounded = false;
    private bool isSprinting = false;
    private bool wasSprinting = false;
    private bool facedAWall = false;
    private bool foundWallOnLeft = false;
    private bool foundWallOnRight = false;
    private bool hasDoubleJumped = false;
    private bool wasOnSky = false;

    void Jump(){
        Vector2 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        movAccel.x = walkAccel;

        lastInputVector = new Vector2(0f, 0f);
    }

    void Update() {
        float mass = rb.mass;
        float playerXDir = (movVel.x/Mathf.Abs(movVel.x));

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector.Normalize();

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (inputVector.x != 0) {
                if (movVel.x/Mathf.Abs(movVel.x) != inputVector.x) movVel.x = 0;
            }

            if (rb.velocity.y <= 4 && !hasDoubleJumped) {
                Jump();
                jumpCount++;
            }
        }

        if (jumpCount > 0) hasDoubleJumped = true;

        if (isGrounded) {
            hasDoubleJumped = false;
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            isSprinting = false;
        }

        // Movimiento

        if (isSprinting) movAccel.x = sprintAccel;
        else movAccel.x = walkAccel;

        if (inputVector.x > 0) movVel.x += movAccel.x * Time.deltaTime * (0.1f);
        else if (inputVector.x < 0) movVel.x -= movAccel.x * Time.deltaTime * (0.1f);
        else {
            if (movVel.x > 0f) movVel.x -= groundDrag * Time.deltaTime * (0.1f);
            else if (movVel.x < 0f) movVel.x += groundDrag * Time.deltaTime * (0.1f);
        }

        if (movVel.x < 0.0001 && movVel.x > -0.0001) movVel.x = 0;

        float maxVel = walkMaxVel;
        if (isSprinting) {
            maxVel = sprintMaxVel;
            wasSprinting = true;
        }

        maxVel /= 40;

        if (inputVector.x != 0) {
            if (!isSprinting && wasSprinting) {
                movVel.x *= 0.98f;
                if (Mathf.Abs(movVel.x) <= walkMaxVel/40) wasSprinting = false;
            }
            else movVel.x = Mathf.Clamp(movVel.x, -maxVel, maxVel);
        }

        if (facedAWall){
            if (foundWallOnLeft && inputVector.x == -1) movVel.x = 0; 
            if (foundWallOnRight && inputVector.x == 1) movVel.x = 0; 
        }

        float maxVelFromSky = limitVelToDriftOnGroundTouch / 40;
        if (Mathf.Abs(movVel.x) < maxVelFromSky && wasOnSky && isGrounded) movVel.x = 0;
    }

    void FixedUpdate() {
        Debug.Log(movVel.x * 40);
        transform.Translate(movVel.x, 0, 0, Space.World);
    }

    void LateUpdate() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        lastInputVector = new Vector2(horizontalInput, verticalInput);
        if (!isGrounded) wasOnSky = true;
        else wasOnSky = false;
    }

    void OnCollisionStay2D(Collision2D collision){
    // Verificar si el objeto con el que colisionó es el suelo
        if (collision.gameObject.CompareTag("Ground")){
            // Obtener todas las contact points de la colisión
            ContactPoint2D[] contacts = collision.contacts;
            
            // Iterar sobre todas las contact points
            bool foundGrounded = false;
            foundWallOnLeft = false;
            foundWallOnRight = false;
            foreach (ContactPoint2D contact in contacts) {
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

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
            facedAWall = false;
            foundWallOnLeft = false;
            foundWallOnRight = false;
        }
    }

}