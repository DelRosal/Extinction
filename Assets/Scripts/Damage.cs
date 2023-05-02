using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public float repelBulletForce = 100f;
    public float repelEnemyForce = 300f;

    private Rigidbody2D rb;
    private Movimiento_new movimiento;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        movimiento = GetComponent<Movimiento_new>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")){
            Debug.Log("Damage");
            anim.SetTrigger("Damage");

            Vector2 direction = (transform.position - collision.transform.position).normalized;
            Vector2 repelForceVec = direction * repelBulletForce;
            movimiento.movVel.x = 0;
            rb.AddForce(repelForceVec, ForceMode2D.Impulse);
        }
        else if (collision.CompareTag("Patrol Enemy")){
            Debug.Log("Damage");
            anim.SetTrigger("Damage");

            Vector2 direction = (transform.position - collision.transform.position).normalized;
            Vector2 repelForceVec = direction * repelEnemyForce;
            movimiento.movVel.x = 0;
            rb.AddForce(repelForceVec, ForceMode2D.Impulse);
        }
        else if (collision.CompareTag("Enemy")){
            Debug.Log("Damage");
            anim.SetTrigger("Damage");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Damage");
            anim.SetTrigger("Damage");
        }
    }
}
