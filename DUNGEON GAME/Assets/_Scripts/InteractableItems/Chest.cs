using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chest Script: Player collects gold coins upon touching
public class Chest : Collectable
{
    public Sprite emptyChest;       // Sprite for the chest
    public int pesosAmount = 5;     // Number of gold coins inside the chest

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;

            // Display UI for gold coins obtained from the chest
            GameManager.instance.ShowText("+" + pesosAmount + " pesos", 25, Color.yellow, transform.position, Vector3.up * 20, 1.5f);

            // Synchronize the amount of gold coins in GameManager
            GameManager.instance.pesos += pesosAmount;
        }
    }
}
