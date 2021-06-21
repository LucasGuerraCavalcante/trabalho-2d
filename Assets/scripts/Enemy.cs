using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2;
    public int enemyHP = 2;
    public int enemyDamage = 2;
    public GameObject[] possibleItensToDrop;
    protected Rigidbody2D rig;
    protected SpriteRenderer spr;
    protected Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
       rig = GetComponent<Rigidbody2D>();
       spr = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (enemyHP > 0) {
            rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
        } else {
            rig.velocity = Vector2.zero;
        }
    }
    public bool TakeDamage(int damage, int dropItemCounter) {
        enemyHP -= damage;
        if (enemyHP <= 0) {
            anim.SetTrigger("dead");

            if (dropItemCounter != 0 && dropItemCounter % 4 == 0) {
                dropItem();
            }

            return true;
        } else {
            anim.SetTrigger("damage");

            return false;
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
        if (other.gameObject.tag == "Player" && enemyHP > 0) {
            anim.SetTrigger("attack");
            other.gameObject.GetComponent<Player>().TakeDamage(enemyDamage);
        }
    }
    void Flip() {
        spr.flipX = !spr.flipX;
    }
    int generateRandomIndex () {
        return (int) Random.Range(0, possibleItensToDrop.Length);
    }
    void dropItem() {
        Instantiate(possibleItensToDrop[generateRandomIndex()], transform.position, Quaternion.identity);
    }
}
