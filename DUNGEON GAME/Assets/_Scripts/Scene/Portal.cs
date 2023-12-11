using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Scene switching script:
//
// Issues encountered: After switching scenes, the background music (BGM) seems to have reduced volume and a change in pitch.
// Attempted solution 1: Move the AudioListener to the Player.
// Result 1: Acceptable, BGM plays relatively normal.

public class Portal : Colliderable
{
    public string sceneName;                // The scene to be loaded
    private SceneTranslate SceneTranslate;  // Scene transition script

    protected override void Start()
    {
        base.Start();
        if (SceneTranslate == null)
            SceneTranslate = GetComponentInChildren<SceneTranslate>();

        GetComponent<BoxCollider2D>().enabled = true;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // Save various information
            GameManager.instance.SaveState();

            // Scene transition to a new scene: Asynchronous loading
            GetComponent<BoxCollider2D>().enabled = false;
            ChangeSceneTo(sceneName);
        }
    }

    public void ChangeSceneTo(string sceneName)
    {
        SceneTranslate.ChangeToScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
