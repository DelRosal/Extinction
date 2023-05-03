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


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }



    IEnumerator DeathFunction()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game Over");
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Enemy"))
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

    
}
