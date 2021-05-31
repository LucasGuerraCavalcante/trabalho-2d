using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obejctSpawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 5, 10);
    }

    // Update is called once per frame
    void Spawn()
    {
        int randIndex = (int) Random.Range(0, obejctSpawn.Length);
        Instantiate(obejctSpawn[randIndex], transform.position, Quaternion.identity);
    }
}
