using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioSource collisonSound;


    //We add the health bar
    public HealthBar healthBar;

    public int maxHealth = 50;
    public int currentHealth;

    //We add sound for the death
    [SerializeField]
    private AudioSource deathSound;

    public float repelBulletForce = 100f;
    public float repelEnemyForce = 300f;

    private Rigidbody2D rb;
    private Movimiento_new movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movimiento = GetComponent<Movimiento_new>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Damage");
            anim.SetTrigger("Damage");

            Vector2 direction = (transform.position - collision.transform.position).normalized;
            Vector2 repelForceVec = direction * repelBulletForce;
            movimiento.movVel.x = 0;
            rb.AddForce(repelForceVec, ForceMode2D.Impulse);
        }
        else if (collision.CompareTag("Patrol Enemy"))
        {
            Debug.Log("Damage");
            anim.SetTrigger("Damage");

            Vector2 direction = (transform.position - collision.transform.position).normalized;
            Vector2 repelForceVec = direction * repelEnemyForce;
            movimiento.movVel.x = 0;
            rb.AddForce(repelForceVec, ForceMode2D.Impulse);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Damage");
            anim.SetTrigger("Damage");
            collisonSound.Play();
            //We call the function to take damage
            TakeDamage(10);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Damage");
            anim.SetTrigger("Damage");
            collisonSound.Play();
            //We call the function to take damage
            TakeDamage(10);
        }


    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            //We only want to play the animation of death once
            anim.SetTrigger("Death");
            Debug.Log("Death");
            //We play the sound of death
            deathSound.Play();

            StartCoroutine(DeathFunction());

        }

    }

    IEnumerator DeathFunction()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game Over");
    }
}
