using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for automatically triggering the door to open:
public class Door : Colliderable
{
    public Sprite doorOpenSprite;

    protected override void OnCollide(Collider2D coll)
    {
        // Check if the collision is with the player
        if (coll.name == "Player")
            // Change the sprite to the open door sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
    }
}
