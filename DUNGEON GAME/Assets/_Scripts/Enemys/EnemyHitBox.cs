﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collectable
{
    [Header("------Damage Parameters------")]
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            Damag dmg = new Damag
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
