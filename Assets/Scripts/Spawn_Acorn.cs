using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Acorn : MonoBehaviour
{//array of enemies
    public GameObject[] aCorn;
    
    public float timeSpawn = 1;

    public float repeatSpawnRate = 8;
    public Transform xAcornRangeLeft;
    public Transform xAcornRangeRight;
    public Transform yAcornRangeUp;
    public Transform yAcornRangeDown;
    

    void Start()
    {
        InvokeRepeating("SpawnAcorn", timeSpawn, repeatSpawnRate);
    }
    public void SpawnAcorn()
    {   
        // We create a random vector to choose a random position 
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        // We create a random number to choose a random enemy between our minimum and maximum float range
        // To spawn our enemy to the left and right in the x axis
        // Our z axis value is = 0, because we only have a 2D game
        spawnPosition = new Vector3(Random.Range(xAcornRangeLeft.position.x, xAcornRangeRight.position.x),
            Random.Range(yAcornRangeDown.position.y, yAcornRangeUp.position.y), 0);
        GameObject enemy = Instantiate(aCorn[Random.Range(0,aCorn.Length)], spawnPosition, gameObject.transform.rotation);
        
        //we print a message in the console to see if our method works
        //Debug.Log("Spawned Enemy");
        
        
    }
}

