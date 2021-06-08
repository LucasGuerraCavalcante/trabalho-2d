using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int activateAfter = 5; // seconds
    public int spawnAgainAfter = 10; // seconds
    public int levelCounter = 0;
    public GameObject[] obejctSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", activateAfter, spawnAgainAfter);
    }

    // Update is called once per frame
    void Spawn()
    {
        spawnEnemy();
        
        levelCounter += 1;

        if (levelCounter == 10) {
            spawnAgainAfter -= 2;

            Invoke("spawnEnemy", 5);
            Invoke("spawnEnemy", 10);
        } else if (levelCounter == 7) {
            spawnAgainAfter -= 2;

            Invoke("spawnEnemy", 5);
        } else if (levelCounter == 5) {
            spawnAgainAfter -= 2;

            Invoke("spawnEnemy", 5);
        } else if (levelCounter == 3) {
            spawnAgainAfter -= 2;
            
            Invoke("spawnEnemy", 5);
        }
    }

    int generateRandomIndex () {
        return (int) Random.Range(0, obejctSpawn.Length);
    }

    void spawnEnemy() {
        Instantiate(obejctSpawn[generateRandomIndex()], transform.position, Quaternion.identity);
    }
}
