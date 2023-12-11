using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI for the equipment menu, including weapon upgrades, character switching, displaying health/level/currency, and experience
public class CharacterMenu : MonoBehaviour
{
    [Header("------Text------")]
    public Text levelTextMenu;
    public Text hitpointText, pesosText, upgradeCostText, xpText;

    [Header("------Sprite/Image------")]
    public Image characterSprite;                       // Player's Sprite
    public Image weaponSprite;                          // Weapon's Sprite
    private int currentCharacterSelection = 0;          // Currently selected PlayerSprite index

    [Header("------Others------")]
    public RectTransform xpBar;                         // Experience bar
    public GameObject savingTextObject;

    // Character switch buttons:
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    // Function to replace the selected Sprite:
    private void OnSelectionChanged()
    {
        characterSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon upgrade button:
    public void OnUpgradeClick()
    {
        // If the condition is true, it means the weapon can be upgraded
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    // Menu update function:
    public void UpdateMenu()
    {
        // Update weapon sprite and the cost to upgrade
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        // If the current weapon level has reached the length of the weapon price table, it means it's upgraded to the maximum level, otherwise, update the displayed cost to upgrade the current weapon
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        // Update character level, health, and currency
        levelTextMenu.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitPoint.ToString() + " /" + GameManager.instance.player.maxHitPoint;
        pesosText.text = GameManager.instance.pesos.ToString();

        // Update XP bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXP = GameManager.instance.GetXPToLevel(currentLevel - 1);
            int currLevelXP = GameManager.instance.GetXPToLevel(currentLevel);

            int diff = currLevelXP - prevLevelXP;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXP;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }

    public void ShowSavingText()
    {
        if (savingTextObject.activeSelf != true)
        {
            savingTextObject.SetActive(true);
            Invoke("HideSavingText", 2);
        }
        else
            return;
    }

    private void HideSavingText()
    {
        savingTextObject.SetActive(false);
    }
}
