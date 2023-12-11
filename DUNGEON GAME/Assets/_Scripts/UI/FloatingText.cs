using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script for displaying floating text information:
public class FloatingText
{
    public bool active;     // Whether it is enabled
    public GameObject go;   // Text object (all under the FloatingTextManager object)
    public Text text;       // Text information
    // public Vector3 targetPos;
    public Vector3 motion;  // Text movement direction
    public float duration;  // Duration of text display
    public float lastshown;

    // Show the text
    public void Show()
    {
        active = true;
        lastshown = Time.time;
        go.SetActive(active);
    }

    // Hide the text
    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    // Refresh the display of the text
    public void UpdateFloatingText()
    {
        if (!active)
            return;

        // Text display duration
        if (Time.time - lastshown > duration)
            Hide();

        // How to make the text fixed at a certain object instead of following the player when moving
        // Debug.Log("go.pos= " + go.transform.position);
        // go.transform.position = Camera.main.WorldToScreenPoint(position);

        // Text display speed
        go.GetComponent<Transform>().position += motion * Time.deltaTime;
        // go.GetComponent<Transform>().position = targetPos;
        // Debug.Log(go.GetComponent<Transform>().position);
        // Debug.Log(targetPos);
    }
}
