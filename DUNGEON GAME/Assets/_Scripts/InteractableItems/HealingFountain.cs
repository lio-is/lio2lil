using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Healing Fountain Script for Restoring Health
/* 
 * Note: Due to the use of AnimatedTile to achieve the healing water effect, 
 * the animator component on healing water needs to be disabled to avoid overlap issues.
 */
public class HealingFountain : Colliderable
{
    public int healingAmount = 1;       // Amount healed each time
    public int healingTotal = 10;       // Total amount of health that can be restored
    private float healCoolDown = 0.5f;  // Cooldown between each healing
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        // Potential bug here:
        // Collision might be detected incorrectly when the fountain collides with wall tiles in the Collision layer of the tilemap
        // Solution: Add additional Collision detection
        if (coll.name == "Player" && GameManager.instance.player.isAlive)
        {
            // Debug.Log("HealingFountain.coll = " + coll.name);
            if (Time.time - lastHeal > healCoolDown && healingTotal > 0)
            {
                Debug.Log(coll.name);

                lastHeal = Time.time;
                healingTotal--;

                GameManager.instance.player.Heal(healingAmount);
            }
        }
        else
            return;
    }
}
