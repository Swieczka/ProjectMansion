using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Knight_Manager : Puzzle_Manager
{
    public Tutorial_Knight[] Knights = new Tutorial_Knight[3];
    [SerializeField] private GameObject Window;
    public override void Check()
    {
        int correct_counter = 0;
        foreach (var knight in Knights)
        {
            if (knight._has_correct_state)
            {
                correct_counter++;
            }
            if (correct_counter == 3)
            {
                Window.GetComponent<Animator>().Play("Base Layer.Open");
                Debug.Log("Dobrze!");
            }
            else
            {
                Window.GetComponent<Animator>().Play("Base Layer.Idle");
            }
        }
    }
    public override void ResetPuzzles()
    {
        foreach (var knight in Knights)
        {
            knight.ResetPuzzle();
            Window.GetComponent<Animator>().Play("Base Layer.Idle");
        }
    }
}
