using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Puzzle
{
    void Start()
    {
        _Manager = GetComponentInParent<Puzzle_Manager>();
        _state = 0;
        PuzzleCheck();

    }
    void Update()
    {
        
    }

    protected override void PuzzleCheck()
    {
        int state = _state % 2;
        switch (state)
        {
            case 0:
                GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 1:
                GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
        _state = state;
        if (_state == _correct_state)
        {
            _has_correct_state = true;
        }
        else
        {
            _has_correct_state = false;
        }
    }

    public override void Action()
    {
        _state++;
        PuzzleCheck();
        _Manager.Check();
    }
}
