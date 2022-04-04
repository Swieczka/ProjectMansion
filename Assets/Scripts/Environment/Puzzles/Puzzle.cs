using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : Interactable
{
    [SerializeField] public int _state;
    [SerializeField] public int _correct_state;
    [SerializeField] public bool _has_correct_state;
    [SerializeField] public Puzzle_Manager _Manager;

    protected virtual void PuzzleCheck()
    {

    }
    public virtual void ResetPuzzle()
    {

    }
}
