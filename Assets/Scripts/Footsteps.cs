using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;

    private Movimiento_new movimiento;

    void Start(){
        movimiento = GetComponent<Movimiento_new>();
    }

    void Update()
    {
        if(movimiento.isGrounded && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            footstepsSound.enabled = true;
        }
        else 
        {
            footstepsSound.enabled = false;
        }
    }
}
