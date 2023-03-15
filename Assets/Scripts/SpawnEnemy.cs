using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    //array of enemies
    public GameObject[] enemies;
    
    public Transform xRangeLeft;
    public Transform xRangeRight;
    public Transform yRangeUp;
    public Transform yRangeDown;
    

    // Update is called once per frame
    void Update()
    {
        // We spwan our enemy onl;y when the "e" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemies();
        }
    }
    public void SpawnEnemies()
    {   
        // We create a random vector to choose a random position 
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        // We create a random number to choose a random enemy between our minimum and maximum float range
        // To spawn our enemy to the left and right in the x axis
        // Our z axis value is = 0, because we only have a 2D game
        spawnPosition = new Vector3(Random.Range(xRangeLeft.position.x, xRangeRight.position.x),
            Random.Range(yRangeDown.position.y, yRangeUp.position.y), 0);
        GameObject enemy = Instantiate(enemies[Random.Range(0,enemies.Length)], spawnPosition, gameObject.transform.rotation);
        
        //we print a message in the console to see if our method works
        //Debug.Log("Spawned Enemy");
        
        
    }
}
