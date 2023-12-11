using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0 : Enemy
{
    public bool someVariable; // Replace this with a meaningful variable name
    public Transform[] fireballs;
    public float[] fireballSpeed = { 2.5f, -2.5f };
    private SpriteRenderer spriteRenderer;

    public float fireballDistance = 0.25f; // Distance between fireballs

    private float startTriggerLength;
    private float startChaseLength;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Record original values for chase parameters
        startTriggerLength = triggerLength;
        startChaseLength = chaseLength;

        // Modify the immune time for this boss
        ImmuneTime = 0.2f;
    }

    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * fireballDistance, Mathf.Sin(Time.time * fireballSpeed[i]) * fireballDistance, 0);
        }

        // When the boss has low health, increase the speed of rotating fireballs, boss speed, and the chase range/distance
        if (((float)hitPoint / (float)maxHitPoint) <= 0.2f)
        {
            fireballSpeed[0] = 4f;
            fireballSpeed[1] = -4f;

            speedMultiple = 1f;
            triggerLength = startTriggerLength * 2;
            chaseLength = startChaseLength * 2;

            spriteRenderer.color = Color.red;
        }
    }
}
