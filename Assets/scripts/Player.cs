using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    }

    void Flip(bool faceRight) {
        spr.flipX = faceRight;
    }
}
