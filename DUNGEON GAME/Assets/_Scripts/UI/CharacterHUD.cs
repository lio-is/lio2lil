using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI in the top-left corner, including current level, health, experience, and rage
public class CharacterHUD : MonoBehaviour
{
    public RectTransform healthBar;     // Health bar
    public RectTransform xpBar;         // Experience bar
    public RectTransform rageBar;       // Rage bar
    public Image rImage;                // R icon

    public Text level;                  // Level text

    // Menu menu update function:
    public void UpdateHUD()
    {
        // Update character level
        level.text = GameManager.instance.GetCurrentLevel().ToString();

        // Update healthBar
        float ratio = (float)GameManager.instance.player.hitPoint / (float)GameManager.instance.player.maxHitPoint;
        healthBar.localScale = new Vector3(ratio, 1, 1);

        // Update XpBar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
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
        }

        // Update RageBar
        rageBar.localScale = new Vector3(GameManager.instance.player.rage / GameManager.instance.player.maxRage, 1, 1);
        if (GameManager.instance.player.rage == GameManager.instance.player.maxRage)
            rImage.gameObject.SetActive(true);
        else
            rImage.gameObject.SetActive(false);
    }
}
