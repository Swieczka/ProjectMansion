using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : Puzzle_Manager
{
    public Painting[] paintings;
    public Lever[] levers;
    public TrapDoor[] trapDoors;
    [SerializeField] private GameObject Gate;
    public override void Check()
    {
        int correct_counter = 0;
        foreach (var painting in paintings)
        {
            if (painting._has_correct_state)
            {
                correct_counter++;
            }
            if (correct_counter == 6)
            {
                Gate.GetComponent<Animator>().Play("Base Layer.Open");
                Debug.Log("Dobrze!");
            }
            else
            {
                Gate.GetComponent<Animator>().Play("Base Layer.Idle");
            }
        }
    }
    public override void ResetPuzzles()
    {
        Gate.GetComponent<Animator>().Play("Base Layer.Idle");
        foreach (var trapdoor in trapDoors)
        {
            trapdoor.ObjectReset();
        }
        foreach(var lever in levers)
        {
            lever.ObjectReset();
        }
        foreach (var painting in paintings)
        {
            painting.ResetPuzzle();
        }
    }
}
