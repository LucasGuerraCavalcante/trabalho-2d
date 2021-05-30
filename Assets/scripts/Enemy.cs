using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2;
    public float enemyHP = 2;
    Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
       rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
    }

    public void TakeDamage(int damage) {
        enemyHP -= damage;
        if (enemyHP <= 0) {
            Destroy(gameObject);
        }
    }
}
