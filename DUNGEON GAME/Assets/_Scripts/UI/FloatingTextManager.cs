using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;        // The FloatingTextManager itself (Panel)
    public GameObject textPrefab;           // FloatingText

    private List<FloatingText> floatingTexts = new List<FloatingText>();    // All currently displayed FloatingTexts


    private void Update()
    {
        // Update Texts in real-time
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    // Function to configure Text information:
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        // Get the Text
        FloatingText floatingText = GetFloatingText();

        // Set various parameters for the Text
        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;

        // Debug.Log("NPC WorldPosition = " + position);

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        // Debug.Log("NPC ScreenPosition = " + floatingText.go.transform.position);
        floatingText.motion = motion;
        // Duration
        floatingText.duration = duration;

        // Position
        // floatingText.targetPos = Camera.main.WorldToScreenPoint(position);

        // Enable the display state
        floatingText.Show();
    }

    // Function to get Text:
    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.text = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }
        return txt;
    }
}
