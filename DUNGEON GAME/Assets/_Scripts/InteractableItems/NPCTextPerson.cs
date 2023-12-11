using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextPerson : Colliderable
{
    public string[] messages;           // All message segments
    private int msgNow = 0;             // Currently displayed message index

    public float showTime;              // Duration for displaying messages
    public float coolDown = 4.0f;       // Cooldown interval for displaying messages
    private float lastShout;

    public bool canLookAtPlayer = false;// Enable always facing the player functionality (not all NPCs rotate)
    private float posDelta;             // Distance difference between player and NPC (only on the x-axis)

    protected override void Start()
    {
        // Initialize cooldown time
        base.Start();
        lastShout = -coolDown;
    }

    protected override void Update()
    {
        base.Update();

        // If the facing player functionality is enabled:
        if (canLookAtPlayer)
        {
            // Switch NPC direction to always face the player
            posDelta = GameManager.instance.player.transform.position.x - transform.position.x;
            if (posDelta > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (posDelta < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Collision-triggered NPC dialogue function: Implement looping through multiple dialogue segments
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player")
            return;

        if (Time.time - lastShout > coolDown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(messages[msgNow++], 30, Color.white, transform.position + new Vector3(0, 0.18f, 0), Vector3.zero, showTime);

            if (msgNow == messages.Length)
                msgNow = 0;
        }
    }
}
