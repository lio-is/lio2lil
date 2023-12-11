using UnityEngine;

public class Enemy_Chest : Enemy
{
    // Use [SerializeField] to make it private in the Inspector but still accessible in the script
    [SerializeField] private Sprite[] sprites;

    // You can use properties to encapsulate the sprite renderer
    private SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

    // The chest enemy reveals its true form only when chasing the player
    protected override void Update()
    {
        base.Update();

        // Use the null-conditional operator to check for null before accessing components
        if (chasing && SpriteRenderer != null)
            SpriteRenderer.sprite = sprites.Length > 1 ? sprites[1] : null;
        else if (SpriteRenderer != null)
            SpriteRenderer.sprite = sprites.Length > 0 ? sprites[0] : null;
    }

    protected override void Death()
    {
        // The chest enemy is destroyed upon death
        Destroy(gameObject);

        // The player gains experience, and a UI with +xp is displayed
        GameManager.instance?.GrantXP(xpValue); // Use the null-conditional operator
        GameManager.instance?.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
