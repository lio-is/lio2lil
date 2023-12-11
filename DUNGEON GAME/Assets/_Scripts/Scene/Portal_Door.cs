using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Portal Script: Transports the object entering this script's GameObject to the vicinity of the boxOUT object.
/* Usage: Set the Rotation.z of the door so that the Player always enters from the +y axis and exits along the +y axis.
 *        You can also use the PortalDoor prefab directly from /_Prefabs/Scene.
 *  Set Parameters:
 *  - Up/Down Portal: R(0,0,180)  R(0,0,0)
 *  - Left/Right Portal: R(0,0,-90)  R(0,0,90)
 */
public class Portal_Door : Colliderable
{
    public BoxCollider2D boxOUT;            // Destination point to teleport to

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // Move in the direction of boxOUT's positive y-axis to prevent triggering teleportation again
            Vector3 vector = boxOUT.transform.position;

            // Adjust Z-axis: Keep Player and others consistently on the Z=0 plane to prevent teleportation bugs with portal_Door
            vector.z = 0;

            // Up/Down Portal:
            if (boxOUT.gameObject.transform.rotation == Quaternion.Euler(0, 0, 0))
                vector.y += 0.2f;
            if (boxOUT.gameObject.transform.rotation == Quaternion.Euler(0, 0, 180))
                vector.y -= 0.2f;

            // Left/Right Portal:
            if (boxOUT.gameObject.transform.rotation == Quaternion.Euler(0, 0, -90))
                vector.x += 0.2f;
            if (boxOUT.gameObject.transform.rotation == Quaternion.Euler(0, 0, 90))
                vector.x -= 0.2f;

            // Set the position to the exit point
            GameManager.instance.player.transform.position = vector;
        }
    }
}
