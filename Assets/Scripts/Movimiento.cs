using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField]
    private float jump_force;

    [SerializeField]
    private float max_walk_speed;

    [SerializeField]
    private float max_run_speed;

    [SerializeField]
    private float transition_speed;

    [SerializeField]
    private float brake_speed;

    [SerializeField]
    private float brake_limit;

    [SerializeField]
    float accel;

    [SerializeField]
    float drag;

    float speed;
    float max_speed;

    float previous_hor;

    bool transition_speed_down = false;
    bool brake = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        max_speed = max_walk_speed;

        previous_hor = Input.GetAxis("Horizontal");

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump")){
            rb.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Run")){
            transition_speed_down = false;
            max_speed = max_run_speed;
        }
        if (Input.GetButtonUp("Run")){
            transition_speed_down = true;
            max_speed = max_walk_speed;
        }
        
        if (horizontal != previous_hor && Input.GetButton("Run") && speed > max_speed/2) {
            brake = true;
            speed *= brake_limit;
        }

        if (horizontal != 0){
            speed += accel * Time.deltaTime * 10;
        }

        else speed -= drag * Time.deltaTime * 10;

        if (!transition_speed_down && speed >= max_speed){
            speed = max_speed;
        }
        if (speed <= 0.1){
            speed = 0;
            previous_hor = horizontal;
        } 

        if (transition_speed_down){
            speed -= transition_speed * Time.deltaTime * 10;
            if (speed <= max_walk_speed){
                transition_speed_down = false;
            }
        }

        if (brake && Input.GetButton("Run")){
            speed -= brake_speed * Time.deltaTime * 10;
            if (speed <= max_speed - (0.8 * max_speed)){
                brake = false;
            }
        }

        transform.Translate(speed * horizontal * Time.deltaTime, 0, 0, Space.World);

        previous_hor = horizontal;
    }
}
