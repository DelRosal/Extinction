using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn_Collected : MonoBehaviour
{
    // We write a function for the character to collect items
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			GetComponent<SpriteRenderer>().enabled = false;
			//We activate the animation of the Acron_collection when the item disapears
			gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject, 0.5f);
		}

	}
}
