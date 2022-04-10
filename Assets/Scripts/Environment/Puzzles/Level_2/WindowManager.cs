using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Puzzle_Manager
{
    [SerializeField] Window[] windows;

    public override void Check()
    {
        int correct_counter = 0;
        foreach (var window in windows)
        {
            if(window._has_correct_state)
            {
                correct_counter++;
            }
        }
        if(correct_counter == windows.Length)
        {
            Debug.Log("Dobrze");
        }
    }
}
