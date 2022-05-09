using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : Puzzle_Manager
{
    [SerializeField] Pipe[] pipes;
    [SerializeField] PipeFinish GateToActivate; 
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
            GateToActivate.Action(true);
        }
        else
        {
            GateToActivate.Action(false); 
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
