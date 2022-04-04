using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : Interactable
{
    [SerializeField] protected int _state;
    [SerializeField] protected int _correct_state;
    [SerializeField] public bool _has_correct_state;
    [SerializeField] public Puzzle_Manager _Manager;

    protected virtual void PuzzleCheck()
    {

    }
    public virtual void ResetPuzzle()
    {

    }
}
