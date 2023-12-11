using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : EnemyHitBox
{
    public float lateToStart = 1f;  // Number of seconds to wait before activating the trap (implementing asynchronous trap arrays)
    private Animator animator;      // The animator for the trap

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        // Start a coroutine to enable the trap after a delay
        StartCoroutine("StartTrap");
    }

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
    }

    IEnumerator StartTrap()
    {
        yield return new WaitForSeconds(lateToStart);
        animator.SetTrigger("start");
    }
}
