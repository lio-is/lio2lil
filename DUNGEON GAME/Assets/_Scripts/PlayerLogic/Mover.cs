using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for movable objects
public abstract class Mover : Fighter
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    // Proportions of speed for horizontal and vertical movement
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    // Record the original size of the object (some enemies may be initially scaled up or down)
    private Vector3 originalSize;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
    }

    // Function to move based on input and speed multiplier
    protected virtual void UpdateMotor(Vector3 input, float speedMultiplier)
    {
        // Movement coordinates: multiplied by speed in each axis
        moveDelta = new Vector3(input.x * xSpeed * speedMultiplier, input.y * ySpeed * speedMultiplier, 0);

        // Change direction: positive/negative
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.y);

        // Move due to pushback
        // The distance pushed back decreases linearly with the pushRecoverSpeed coefficient
        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Directional movement: up, down, left, right
        // Only detectable if the object is on the Blocking or Actor layer and has a BoxCollider2D
        // Mechanism: Create a detection matrix with the same size as the boxcollider2D, extend the matrix towards the coordinates to be reached,
        //            if there is an object on the specified layer, return the first object in contact; otherwise, return null, indicating that it can move to the destination
        //
        // The following hidden BUG:
        // Problem: When attacked by an enemy, causing a knockback effect, and the Player is close to a wall, it gets pushed out
        // Problem analysis: The problem lies in the pushDirection movement detection
        // Solution: Add a check, when colliding with a wall, set the received pushDirection to zero
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        else
            pushDirection = Vector3.zero;

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        else
            pushDirection = Vector3.zero;
    }
}
