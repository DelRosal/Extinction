using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    // We create a variable to store the horizontal movement
    float horizontalMove = 0f;

    // We create a variable to store the speed of the character
    public float runSpeed = 40f;

    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // To make our character Jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    // We create a function for our charachter to know when he es landing from a jump
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }




    void FixedUpdate()
    {
        // Moving our character
        // Moving our character according to our variable 'horizontalMove'
        // We write false to daclare that the character is not jumping
        // We write another false to declare that the charater is not crouching
        // We multiplyed the variable for Deltatime, for the speed to be consistent
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        // We set the jump variable to false, so that the character doesn't jump in a loop forever
        jump = false;
    }
}
