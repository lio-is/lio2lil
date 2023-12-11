using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("------Contained Panels------")]
    public CharacterMenu characterMenu;             // Equipment menu (bottom-left corner)
    public CharacterHUD characterHUD;               // Health and experience menu (top-left corner)
    public FloatingTextManager floatingTextManager; // Text display manager

    [Header("------Special State Machines------")]
    public Animator deathMenuAnim;                  // Death screen animation

    private void Start()
    {
        deathMenuAnim.gameObject.SetActive(false);
        UIUpdate();
    }

    // Update game value UI
    public void UIUpdate()
    {
        characterMenu.UpdateMenu();
        characterHUD.UpdateHUD();
    }

    // Common function to display text information:
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Hide death animation
    public void HideDeathAnimation()
    {
        deathMenuAnim.SetTrigger("Hide");
        deathMenuAnim.gameObject.SetActive(false);
    }

    // Play death animation
    public void ShowDeathAnimation()
    {
        deathMenuAnim.gameObject.SetActive(true);
        deathMenuAnim.SetTrigger("Show");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
