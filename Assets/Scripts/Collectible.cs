using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioSource collectSound;
    // We write a function for the character to collect items
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            collectSound.Play();
            Destroy(collision.gameObject);
            SceneManager.LoadScene("Level4");
        }

    }
}
