using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // HP
    public int HP = 10;
    public int maxHP = 10;
    public bool isAlive = true;
    // Walk
    public float moveSpeed = 5.0f;
    // Jump
    public float jumpForce = 500;
    public Transform groundCheck;
    public float circleOverlapRadius = 0.2f;
    public LayerMask whatIsGround;
    // Components
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spr;
    // Other
    public int dropItemCounter = 0;
    private Enemy enemyInArea = null;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            // Walk
            float h = Input.GetAxis("Horizontal");
            rig.velocity = new Vector2(h*moveSpeed, rig.velocity.y); 
            anim.SetFloat("speed", Mathf.Abs(h));
            // Flip Player
            if (h > 0) Flip(false); else if (h < 0) Flip(true);
            // Jump
            bool isOnGround = Physics2D.OverlapCircle(groundCheck.position, circleOverlapRadius, whatIsGround);
            if (Input.GetButtonDown("Jump") && isOnGround) {
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            }

            anim.SetBool("onGround", isOnGround);

            // Attacks
            if (Input.GetKeyDown(KeyCode.J)) {
                AttackAnimation("atk01"); // Up to down axe attack
            }           
            if (Input.GetKeyDown(KeyCode.K)) {
                AttackAnimation("atk02"); // Side axe attack
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                AttackAnimation("atk03"); // Spin axe attack
            }
        }
    }
    void AttackAnimation(string atkType) {
        anim.SetTrigger(atkType);
    }
    public void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0) {
            anim.SetTrigger("dead");
            isAlive = false;
            GetComponent<CapsuleCollider2D>().enabled = false; 
        } else {
            anim.SetTrigger("damage");
        }
    }
    public void AttackEnemy(string atkType) {
        if (enemyInArea != null && enemyInArea.enemyHP > 0) {
            if (atkType == "atk01") {
                bool attackAndDeadCheck = enemyInArea.TakeDamage(3, dropItemCounter);

                if (attackAndDeadCheck == true) {
                    dropItemCounter += 1;
                }
            };
            if (atkType == "atk02") {
                bool attackAndDeadCheck = enemyInArea.TakeDamage(2, dropItemCounter);

                if (attackAndDeadCheck == true) {
                    dropItemCounter += 1;
                }
            };
            if (atkType == "atk03") {
                bool attackAndDeadCheck = enemyInArea.TakeDamage(2, dropItemCounter);

                if (attackAndDeadCheck == true) {
                    dropItemCounter += 1;
                }
            };
        }
    }
    void Flip(bool isFacingLeft) {
        spr.flipX = isFacingLeft;
    }
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == 7) {
            enemyInArea = other.GetComponent<Enemy>();
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 7) {
            enemyInArea = null;
        }
        if (other.gameObject.layer == 8) {
            if (HP < maxHP) {
                HP = maxHP;
                Destroy(other.gameObject);
            }
        }
    }
}
