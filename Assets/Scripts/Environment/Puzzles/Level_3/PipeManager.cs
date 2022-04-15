using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : Puzzle_Manager
{
    [SerializeField] Pipe[] pipes;

    public override void Check()
    {
        int correct_counter = 0;
        foreach (Pipe pipe in pipes)
        {
            if (pipe._has_correct_state)
            {
                correct_counter++;
            }
        }
        if (correct_counter == pipes.Length)
        {
            Debug.Log("Dobrze!");
        }
    }

    public override void ResetPuzzles()
    {
        foreach (Pipe pipe in pipes)
        {
            pipe.ObjectReset();
        }
    }
}
