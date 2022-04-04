using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : Puzzle
{
    [SerializeField] Vector3 objectPos;
    private void Awake()
    {
        objectPos = transform.position;
        _Manager = GetComponentInParent<Puzzle_Manager>();
    }
    private void Update()
    {
        
    }
    protected override void PuzzleCheck()
    {
       if(_state == _correct_state)
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
        PuzzleCheck();
        _Manager.Check();
    }

    public override void ResetPuzzle()
    {
        gameObject.transform.position = objectPos;
    }

    public override void ObjectReset()
    {
        gameObject.transform.position = objectPos;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
