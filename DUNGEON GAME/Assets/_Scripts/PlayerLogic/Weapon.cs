using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Weapon attack script: Implements attacking by reading input keys
public class Weapon : Colliderable
{
    // Damage parameters for each weapon level:
    [Header("------Damage Parameters------")]
    public int[] damagePoint = { 4, 5, 6, 7, 8, 9, 10 };                         // Damage dealt by the weapon
    public float[] pushForce = { 3.0f, 3.2f, 3.5f, 4.0f, 4.3f, 4.6f, 5.0f };    // Force applied by the weapon

    // Weapon level parameters:
    [Header("------Level Parameters------")]
    public int weaponLevel = 0;             // Current weapon level
    private SpriteRenderer SpriteRenderer;  // Sprite of the current weapon

    // Weapon control parameters:
    [Header("------Control Parameters------")]
    public Animator animator;              // Animation component
    private float swingCoolDown = 0.4f;   // Cooldown time for weapon attacks
    private float lastSwing;

    // Rage skill parameters:
    //- Rage skill effects: Basic attack releases a flaming sword, dealing ranged damage to enemies
    //- Requirements for using the Rage skill: Accumulate rage by taking damage within the player, release the skill when rage is full
    [Header("------Rage Skill Parameters------")]
    public GameObject flamingSword;         // Flaming sword
    public GameObject rageState;            // Skill effect
    public bool CanRageSkill = false;       // Whether the skill can be used
    public bool raging = false;             // Whether the skill is currently being used
    public float ragingTime = 4f;           // Duration of the skill

    private void Awake()
    {
        // Bug occurrence: Race condition issue
        // Problem: If GetComponent<> is called in Start, it causes a bug.
        // Analysis: This is because GameManager's Awake is already executed by SceneManager.sceneLoaded += LoadState, 
        //            which means SetWeaponLevel(int.Parse(data[3])) is executed, 
        //            but at this point, SpriteRenderer component is obtained in Start, which is later than GameManager's Awake.
        // Solution: Be mindful of the order of Awake and Start.
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rageState.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        // Read input: Spacebar / Left mouse button to perform basic attack
        if (GameManager.instance.player.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (Time.time - lastSwing > swingCoolDown)
                {
                    lastSwing = Time.time;

                    // Weapon swing animation               
                    Swing();

                    // Release skill
                    if (raging)
                    {
                        CreateFlamingSword();
                    }
                    else
                        rageState.SetActive(false);
                }
            }

            // When the conditions for releasing the skill are met and the skill is not currently being used
            if (Input.GetKeyDown(KeyCode.R) && (!raging))
            {
                if (CanRageSkill)
                {
                    raging = true;
                    rageState.SetActive(true);
                    StartCoroutine("WaitingForRestRageSkill");
                }
            }
        }
    }

    // Weapon collision causing damage function:
    // Note: The BoxCollider is disabled when in the idle state, so it can only detect collisions when in the Swing state
    protected override void OnCollide(Collider2D coll)
    {
        // Only objects that can be damaged should undergo damage checks
        if (coll.CompareTag("Fighter"))
        {
            // The weapon should not damage the player
            if (coll.name == "Player")
                return;

            // Otherwise, it's an enemy
            Damag dmg = new Damag
            {
                // Damage and push force dealt to the enemy are determined by the weapon's level
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            // Send a message to the collided object to call its ReceiveDamage function (inside the Fighter class)
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    // Set Animator state function: Weapon swing
    private void Swing()
    {
        //----------------------------------
        // Some suggestions about the Swing animation:
        // 1. The time of the weapon's Swing animation should be greater than 0.3s; otherwise, the interval is too short,
        //    and even if the enemy can be attacked every time, it is not enough to effectively push the enemy away before being attacked.
        // 2. The time to swing the weapon horizontally should be completed in the first half of the time,
        //    because it involves the weapon's collision damage:
        //    The range of the weapon's collider is longer when it is swung horizontally, which is helpful to maintain distance from the enemy and deal damage.
        //
        // Additional notes:
        // 3. It's best to simulate a real swinging action. I divide it into the following parts:
        //    p1: The weapon is lifted and tilted backward. The speed during this process is moderate.
        //    p2: The main swinging action from 90' to 0'. The speed during the first half is fast, and during the second half is very fast.
        //    p3: The final swinging action from 0' to -20'. The speed during this process is not about speed but about maintaining frames (time).
        // The above settings can also be deliberately adjusted based on the difficulty.
        //----------------------------------
        animator.SetTrigger("Swing");
    }

    // Create FlamingSword object
    private void CreateFlamingSword()
    {
        GameObject go = Instantiate(flamingSword);
    }

    // Upgrade weapon
    public void UpgradeWeapon()
    {
        // Level up, change sprite
        weaponLevel++;
        SpriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    // Set weapon level
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        SpriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    IEnumerator WaitingForRestRageSkill()
    {
        yield return new WaitForSeconds(ragingTime);
        raging = false;
        CanRageSkill = false;
        GameManager.instance.player.rage = 0;
        GameManager.instance.OnUIChange();
    }
}
