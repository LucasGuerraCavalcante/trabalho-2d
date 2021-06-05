using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2;
    public int enemyHP = 2;
    public int enemyDamage = 2;
    Rigidbody2D rig;
    SpriteRenderer spr;
    Animator anim; 


    // Start is called before the first frame update
    void Start()
    {
       rig = GetComponent<Rigidbody2D>();
       spr = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP > 0) {
            rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
        } else {
            rig.velocity = Vector2.zero;
        }
    }

    public void TakeDamage(int damage) {
        enemyHP -= damage;
        if (enemyHP <= 0) {
            anim.SetTrigger("dead");
        } else {
            anim.SetTrigger("damage");
        }
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "EnemyWall") {
            moveSpeed *= -1;
            Flip();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            anim.SetTrigger("attack");
            other.gameObject.GetComponent<Player>().TakeDamage(enemyDamage);
        }
    }

    void Flip() {
        spr.flipX = !spr.flipX;
    }
}
