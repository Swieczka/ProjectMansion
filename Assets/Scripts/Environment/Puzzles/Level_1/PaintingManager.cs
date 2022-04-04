using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : Puzzle_Manager
{
    public Painting[] paintings;
    public Lever[] levers;
    public TrapDoor[] trapDoors;
    [SerializeField] private GameObject Window;
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
                Debug.Log("Dobrze!");
            }
        }
    }
    public override void ResetPuzzles()
    {
        foreach (var painting in paintings)
        {
            painting.ResetPuzzle();
        }
        foreach (var trapdoor in trapDoors)
        {
            trapdoor.ObjectReset();
        }
        foreach(var lever in levers)
        {
            lever.ObjectReset();
        }
    }
}
