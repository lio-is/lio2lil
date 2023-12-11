using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDisplayControl : MonoBehaviour
{
    public GameObject enemies;  // Reference to the enemies GameObject
    public GameObject trans;    // Reference to the road GameObject that appears after defeating all enemies
    private int num;            // Variable to store the number of enemies

    private void Start()
    {
        // Deactivate the road GameObject at the start
        trans.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Activate the road when all enemies are eliminated
        num = enemies.transform.childCount;

        if (num == 0)
        {
            DisplayRoad();
        }
    }

    // Activate the road GameObject
    public void DisplayRoad()
    {
        trans.gameObject.SetActive(true);
    }
}
