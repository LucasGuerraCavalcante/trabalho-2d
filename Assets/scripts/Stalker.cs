using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : Enemy
{
    private Transform playerPosition;

    void Awake() {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    new void Update() {
        if (transform.position.x >= playerPosition.position.x) {
            rig.velocity = new Vector2(-moveSpeed, rig.velocity.y);
        }
        else if (transform.position.x < playerPosition.position.x) {
            rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
        }

        if (transform.position.y >= playerPosition.position.y) {
            rig.velocity = new Vector2(rig.velocity.x, -moveSpeed);
        }
        else if (transform.position.y < playerPosition.position.y) {
            rig.velocity = new Vector2(rig.velocity.x, moveSpeed);
        }
    }
}
