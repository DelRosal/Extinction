using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // We write a function for the character to collect items
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Collectable"))
		{
			
            Destroy(collision.gameObject);
		}

	}
}
